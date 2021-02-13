using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    #region  Değişkenler
    public float hareketHizi, ziplamaGucu, donmeHizi;
    public bool yasiyormu = true;
    public Material olumMateryal;
    public Material hologramMateryal;
    public Material[] teleportMat;
    public GameObject Body;
    public GameObject Kalkan;

    [ColorUsageAttribute(true, true)]
    public Color yokOlmaRengi;
    [ColorUsageAttribute(true, true)]
    public Color dogmaRengi;
    public GameObject teleportKonum;
    public GameObject teleportBilgiText;

    private Vector3 baktigiYon;
    private bool yerdemi;
    private Rigidbody _rb;
    private Renderer _renderer;
    private Material[] _eskiMateryaller;
    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private float bonusZamani;
    private int bonusAlindimi = 0;
    private bool isinlaniyormu = false;

    #endregion
    #region Temel Fonksiyonlar
    void Awake()
    {
        _renderer = Body.GetComponent<Renderer>();
        _eskiMateryaller = _renderer.materials;
        KarakterMateryalDegistir();
        KarakterMateryalFloatDegistir(1);
        KarakterMateryalEfektRenkDegistir(dogmaRengi);


    }
    void Start()
    {
        StartCoroutine(DogumEfekti());
        _rb = this.GetComponent<Rigidbody>();
        _animator = this.GetComponent<Animator>();
        _capsuleCollider = this.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (yasiyormu)
        {
            KarakterZiplat();
            KarakterHareket();
            BonusKontrol();
            TeleportMateryalDegistir();

        }


    }
    void TeleportMateryalDegistir()
    {
        if (teleportKonum.transform.position.x < 8 && teleportKonum.transform.position.x > -8)
        {
            teleportKonum.GetComponent<Renderer>().material = teleportMat[0];

        }
        else
        {
            teleportKonum.GetComponent<Renderer>().material = teleportMat[1];

        }
    }
    void Update()
    {
        if (bonusAlindimi == 3 && isinlaniyormu == false)
        {
            KarakterIsinla();
        }
    }
    void OnCollisionStay(Collision coll)
    {
        if (coll.transform.tag == "zemin")
        {
            yerdemi = true;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "enemy")
        {
            KarakterOldur();
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "teleport")
        {
            StartCoroutine(Isinlan());
        }
        if (coll.transform.tag == "coin")
        {
            bonusZamani = 10f;
            KarakterBonusAldi(coll.GetComponent<Coin>().alinanBonus);
            Destroy(coll.transform.gameObject);
        }
    }
    #endregion
    #region Ozel Fonksiyonlar

    void KarakterIsinla()
    {
        if (yasiyormu == true && Input.GetKeyDown(KeyCode.LeftShift) && teleportKonum.transform.position.x > -6 && teleportKonum.transform.position.x < 6)
        {
            Debug.Log(teleportKonum.transform.position);
            StartCoroutine(TeleportYetenegi());
        }
    }
    void KarakterBonusAldi(int AlinanBonus)
    {
        bonusAlindimi = AlinanBonus;
        if (bonusAlindimi == 1)
        {
            this.gameObject.layer = 0;
            if (Kalkan.GetComponent<Kalkan>().kalkanDurumu)
            {
                Kalkan.GetComponent<Kalkan>().KalkanKapatirmisinLayerOlmadan();
            }
            if (teleportKonum.activeSelf == true)
            {
                teleportKonum.SetActive(false);
            }
            KarakterMateryalHologramYap();
        }
        else if (bonusAlindimi == 2)
        {
            this.gameObject.layer = 0;
            KarakterMateryalDegistir();
            DogumEfekti();
            if (teleportKonum.activeSelf == true)
            {
                teleportKonum.SetActive(false);
            }
            Kalkan.GetComponent<Kalkan>().KalkanAcarmisin();
        }
        else if (bonusAlindimi == 3)
        {
            this.gameObject.layer = 8;
            if (Kalkan.GetComponent<Kalkan>().kalkanDurumu)
            {
                Kalkan.GetComponent<Kalkan>().KalkanKapatirmisinLayerOlmadan();
            }
            teleportBilgiText.SetActive(true);
            KarakterMateryalDegistir();
            teleportKonum.SetActive(true);
        }
    }
    void KarakterBonusBitti()
    {
       
        if (bonusAlindimi == 1)
        {
            this.gameObject.layer = 8;
            KarakterMateryalDegistir();
            StartCoroutine(DogumEfekti());
        }
        else if (bonusAlindimi == 2)
        {
            Kalkan.GetComponent<Kalkan>().KalkanKapatirmisin();
        }
        else if (bonusAlindimi == 3)
        {
            teleportKonum.SetActive(false);
            teleportBilgiText.GetComponent<Text>().enabled = false;

        }
        bonusAlindimi = 0;
    }
    void BonusKontrol()
    {
        if (bonusZamani > 0)
        {
            bonusZamani -= Time.deltaTime;
        }
        else if (bonusAlindimi != 0)
        {
            KarakterBonusBitti();
        }
    }
    public void KarakterOldur()
    {
        yasiyormu = false;
        OlumAnimasyonu();
        KarakterMateryalEfektRenkDegistir(yokOlmaRengi);
        StartCoroutine(OlumEfekti(1));
    }
    void KarakterMateryalFloatDegistir(float i)
    {
        _renderer.materials[0].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
        _renderer.materials[1].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
        _renderer.materials[2].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
        _renderer.materials[3].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
        _renderer.materials[4].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
        _renderer.materials[5].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
    }
    void HologramMateryalFloatDegistir(float i)
    {
        _renderer.materials[0].SetFloat("Vector1_faf16ca62dc643269b8340ab300f5744", i);
        _renderer.materials[1].SetFloat("Vector1_faf16ca62dc643269b8340ab300f5744", i);
        _renderer.materials[2].SetFloat("Vector1_faf16ca62dc643269b8340ab300f5744", i);
        _renderer.materials[3].SetFloat("Vector1_faf16ca62dc643269b8340ab300f5744", i);
        _renderer.materials[4].SetFloat("Vector1_faf16ca62dc643269b8340ab300f5744", i);
        _renderer.materials[5].SetFloat("Vector1_faf16ca62dc643269b8340ab300f5744", i);
    }
    void KarakterMateryalHologramYap()
    {
        _renderer.materials = new Material[6] { hologramMateryal, hologramMateryal, hologramMateryal, hologramMateryal, hologramMateryal, hologramMateryal };
    }
    void KarakterMateryalDegistir()
    {
        _renderer.materials = new Material[6] { olumMateryal, olumMateryal, olumMateryal, olumMateryal, olumMateryal, olumMateryal };
        _renderer.materials[0].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", _eskiMateryaller[0].color);
        _renderer.materials[1].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", _eskiMateryaller[1].color);
        _renderer.materials[2].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", _eskiMateryaller[2].color);
        _renderer.materials[3].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", _eskiMateryaller[3].color);
        _renderer.materials[4].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", _eskiMateryaller[4].color);
        _renderer.materials[5].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", _eskiMateryaller[5].color);


    }
    void KarakterMateryalEfektRenkDegistir(Color color)
    {
        _renderer.materials[0].SetColor("Color_fac2b1d2135c4e6899821cfd945b496c", color);
        _renderer.materials[1].SetColor("Color_fac2b1d2135c4e6899821cfd945b496c", color);
        _renderer.materials[2].SetColor("Color_fac2b1d2135c4e6899821cfd945b496c", color);
        _renderer.materials[3].SetColor("Color_fac2b1d2135c4e6899821cfd945b496c", color);
        _renderer.materials[4].SetColor("Color_fac2b1d2135c4e6899821cfd945b496c", color);
        _renderer.materials[5].SetColor("Color_fac2b1d2135c4e6899821cfd945b496c", color);
    }
    public IEnumerator OlumEfekti(float beklemeSuresi)
    {
        yield return new WaitForSeconds(beklemeSuresi);
        for (float i = -0.5f; i <= 1; i += 0.01f)
        {
            if (bonusAlindimi == 1)
            {
                HologramMateryalFloatDegistir(i);
            }
            else if (bonusAlindimi == 2)
            {
                Kalkan.GetComponent<Kalkan>().KalkanKapatirmisin();
                KarakterMateryalFloatDegistir(i);

            }
            else if (bonusAlindimi == 0)
            {
                KarakterMateryalFloatDegistir(i);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Isinlan()
    {
        if (bonusAlindimi == 1)
        {
            for (float i = -1f; i <= 1; i += 0.05f)
            {

                HologramMateryalFloatDegistir(i);

                yield return new WaitForSeconds(0.01f);
            }
            this.transform.position += new Vector3(0, 4, 7);
            for (float i = +1f; i >= -1; i -= 0.05f)
            {

                HologramMateryalFloatDegistir(i);

                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (bonusAlindimi == 0 || bonusAlindimi == 2 || bonusAlindimi == 3)
        {
            for (float i = -0.5f; i <= 1; i += 0.1f)
            {

                KarakterMateryalFloatDegistir(i);

                yield return new WaitForSeconds(0.01f);
            }
            this.transform.position += new Vector3(0, 4, 7);
            for (float i = +1f; i >= -1; i -= 0.1f)
            {

                KarakterMateryalFloatDegistir(i);


                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    IEnumerator TeleportYetenegi()
    {
        isinlaniyormu = true;
        for (float i = -0.5f; i <= 1; i += 0.1f)
        {

            KarakterMateryalFloatDegistir(i);

            yield return new WaitForSeconds(0.01f);
        }

        this.transform.position = new Vector3(teleportKonum.transform.position.x, this.transform.position.y, this.transform.position.z);

        for (float i = +1f; i >= -0.5; i -= 0.1f)
        {

            KarakterMateryalFloatDegistir(i);


            yield return new WaitForSeconds(0.01f);
        }
        isinlaniyormu = false;
    }

    IEnumerator DogumEfekti()
    {

        for (float i = +1f; i >= -1; i -= 0.1f)
        {
            KarakterMateryalFloatDegistir(i);

            yield return new WaitForSeconds(0.01f);
        }
    }
    void KarakterZiplat()
    {
        float y = Input.GetAxis("Jump");
        //Debug.Log(yerdemi);
        if (yerdemi && y > 0)
        {
            ZiplamaAnimasyonu(true);
            yerdemi = false;
            //Debug.Log("Karakter zıpladı");
            _rb.AddForce(Vector3.up * ziplamaGucu);

        }
        else
        {
            ZiplamaAnimasyonu(false);

        }

    }
    void KarakterHareket()
    {
        float hareketX, hareketZ;
        hareketX = Input.GetAxis("Horizontal");
        hareketZ = Input.GetAxis("Vertical");
        baktigiYon = new Vector3(hareketX, 0, hareketZ);

        if (hareketX != 0 || hareketZ != 0)
        {
            this.transform.position += new Vector3(hareketHizi * hareketX * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(baktigiYon), donmeHizi * Time.deltaTime);
            KosmaAnimasyonu(true);
        }
        else
        {
            KosmaAnimasyonu(false);

        }

    }

    void ZiplamaAnimasyonu(bool x)
    {
        _animator.SetBool("zipla", x);
    }
    void YurumeAnimasyonu(bool x)
    {
        _animator.SetBool("yuru", x);
    }
    void KosmaAnimasyonu(bool x)
    {
        _animator.SetBool("kos", x);
    }
    void OlumAnimasyonu()
    {
        _animator.SetBool("olum", true);
    }
    #endregion

}
