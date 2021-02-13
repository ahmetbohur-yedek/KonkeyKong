using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public int alinanBonus;
    public GameObject kalkan;
    public Material hologram;
    public Material teleport;
    private Material[] ilkMateryaller;
    private Renderer _renderer;
    void Awake()
    {
        kalkan.SetActive(false);
        _renderer = this.GetComponent<Renderer>();
        ilkMateryaller = _renderer.materials;
        alinanBonus = Random.Range(1, 4);
        float Konum = Random.Range(-3, 3.1f);
        this.transform.Translate(Konum, 0, 0);
        BonusMateryalDegistir();
    }
    void BonusMateryalDegistir()
    {
        if (alinanBonus == 1)
        {
            _renderer.materials = new Material[2] { hologram, hologram };
        }
        if (alinanBonus == 2)
        {
            kalkan.SetActive(true);
        }
        if (alinanBonus == 3)
        {
            _renderer.materials = new Material[2] { teleport, teleport };
            _renderer.materials[0].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", ilkMateryaller[0].color);
            _renderer.materials[1].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", ilkMateryaller[1].color);
        }
    }
    void Start()
    {
        int OlusmaIhtimali = Random.Range(0, 100);
        if (OlusmaIhtimali >= 30)
        {
            Destroy(this.gameObject);
        }
        if(alinanBonus == 3){
             StartCoroutine(KupTeleportEt());
        }
    }

    // Update is called once per frame
    void Update()
    {
        CoinDondur();
        KalkanRengiGuncelle();
    }
    void KalkanRengiGuncelle()
    {
        kalkan.GetComponent<Renderer>().material.SetColor("Color_8c788506be1d4252add11f2d7aec4108", GameObject.FindGameObjectWithTag("oyuncuKalkan").GetComponent<Kalkan>().randomRenk);
    }
    void CoinDondur()
    {
        this.transform.Rotate(0, 0, 360 * Time.deltaTime);
    }
    void KarakterMateryalFloatDegistir(float i)
    {
        _renderer.materials[0].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
        _renderer.materials[1].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
    }
    IEnumerator KupTeleportEt()
    {
        for (float i = -0.5f; i <= 1; i += 0.1f)
        {

            KarakterMateryalFloatDegistir(i);

            yield return new WaitForSeconds(0.01f);
        }
        if (this.transform.localPosition.x <= 6 && this.transform.localPosition.x >= -6)
        {
            float Konum = Random.Range(-3, 3.1f);
            this.transform.position += new Vector3(Konum, 0 ,0);
        }
        for (float i = +1f; i >= -1; i -= 0.1f)
        {

            KarakterMateryalFloatDegistir(i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(KupTeleportEt());
    }
}
