using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KameraTakip : MonoBehaviour
{
    public GameObject _karakter;
    public GameObject[] paneller;
    void FixedUpdate()
    {
        KonumHesapla();
    }
    private bool kameraCevir = true;
    private AudioSource _AS;

    void Awake()
    {
        _AS = this.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Ses") == 0)
        {
            _AS.volume = 1;
        }
        else if (PlayerPrefs.GetInt("Ses") == 1)
        {
            _AS.volume = 0;
        }
    }
    void KonumHesapla()
    {
        // -5 ve 5 arasında gidip gelecek
        // -9 ve 9
        if (_karakter.GetComponent<Karakter>().yasiyormu == true)
        {
            this.transform.position = new Vector3(_karakter.transform.position.x, _karakter.transform.position.y + 4.5f, _karakter.transform.position.z - 6);
        }
        else if (kameraCevir)
        {
            kameraCevir = false;
            StartCoroutine(KameraYenidenKonumlandir());

        }
    }
    IEnumerator KameraYenidenKonumlandir()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 200; i++)
        {
            this.transform.eulerAngles += new Vector3(-0.335f, 0.9f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        paneller[0].SetActive(false);
        paneller[1].SetActive(true);
        StartCoroutine(KaybettinOynanirFillDegistir());
    }

    IEnumerator KaybettinOynanirFillDegistir()
    {
        paneller[3].SetActive(false);

        for (float i = 0; i <= 1; i += 0.01f)
        {
            paneller[1].GetComponent<Image>().fillAmount = i;
            paneller[2].GetComponent<Image>().fillAmount = i;

            yield return new WaitForSeconds(0.01f);
        }
        paneller[1].GetComponent<Image>().fillAmount = 1;
        paneller[2].GetComponent<Image>().fillAmount = 1;
        paneller[3].SetActive(true);

    }
}
