using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DilSistemi : MonoBehaviour
{
    public Kelimeler kelimeler;
    public AudioSource _audioSource;
    public GameObject[] paneller;
    public GameObject[] fillPaneller;
    public Text[] _Texts;
    int dil;
    private bool panelKontrol;

    void Awake()
    {


        dil = PlayerPrefs.GetInt("Dil");
        if (dil == 0)
        {
            if (Application.systemLanguage == SystemLanguage.Turkish)
            {
                kelimeler.Turkce();
                _Texts[4].text = kelimeler.turkce;
            }
            else if (Application.systemLanguage == SystemLanguage.English)
            {
                kelimeler.Ingilice();
                _Texts[4].text = kelimeler.ingilizce;

            }
        }
        if (dil == 1)
        {
            kelimeler.Turkce();
            _Texts[4].text = kelimeler.turkce;

        }
        if (dil == 2)
        {
            kelimeler.Ingilice();
            _Texts[4].text = kelimeler.ingilizce;

        }

        if (PlayerPrefs.GetInt("Ses") == 0)
        {
            _audioSource.volume = 1;
        }
        else if (PlayerPrefs.GetInt("Ses") == 1)
        {
            _audioSource.volume = 0;
        }

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _Texts[1].enabled = false;
            _Texts[0].enabled = false;
            _Texts[5].enabled = false;

            _Texts[1].text = kelimeler.yuksekPuan + "\n" + PlayerPrefs.GetInt("YPuan").ToString();
            _Texts[0].text = kelimeler.baslangicEkraniAciklamasi;

            StartCoroutine(BaslangicPanelFillDegistir());
            if (PlayerPrefs.GetInt("Ses") == 0)
                _Texts[2].text = kelimeler.sesKapat;
            else if (PlayerPrefs.GetInt("Ses") == 1)
            {
                _Texts[2].text = kelimeler.sesAc;
            }
            _Texts[3].text = kelimeler.nasilOynanir;
            _Texts[5].text = kelimeler.nasilOynanirAciklama;

        }

    }
    public void SesAcKapat()
    {
        if (PlayerPrefs.GetInt("Ses") == 1)
        {

            PlayerPrefs.SetInt("Ses", 0);
            _audioSource.volume = 1;
            _Texts[2].text = kelimeler.sesKapat;
        }
        else
        {
            PlayerPrefs.SetInt("Ses", 1);
            _audioSource.volume = 0;
            _Texts[2].text = kelimeler.sesAc;

        }

    }
    public void DilDegistir()
    {
        if (dil == 0 && Application.systemLanguage == SystemLanguage.Turkish || dil == 1)
        {
            _Texts[4].text = kelimeler.ingilizce;
            PlayerPrefs.SetInt("Dil", 2);
            kelimeler.Ingilice();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        }
        else if (dil == 0 && Application.systemLanguage == SystemLanguage.English || dil == 2)
        {
            PlayerPrefs.SetInt("Dil", 1);
            _Texts[4].text = kelimeler.turkce;
            kelimeler.Turkce();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        }

    }

    IEnumerator BaslangicPanelFillDegistir()
    {
        _Texts[0].enabled = false;
        _Texts[1].enabled = false;
        for (float i = 0; i <= 1; i += 0.01f)
        {
            fillPaneller[0].GetComponent<Image>().fillAmount = i;
            fillPaneller[1].GetComponent<Image>().fillAmount = i;

            yield return new WaitForSeconds(0.01f);
        }
        fillPaneller[0].GetComponent<Image>().fillAmount = 1;
        fillPaneller[1].GetComponent<Image>().fillAmount = 1;
        _Texts[0].enabled = true;
        _Texts[1].enabled = true;



    }
    IEnumerator NasilOynanirFillDegistir()
    {
        _Texts[5].enabled = false;

        for (float i = 0; i <= 1; i += 0.01f)
        {
            fillPaneller[2].GetComponent<Image>().fillAmount = i;
            fillPaneller[3].GetComponent<Image>().fillAmount = i;

            yield return new WaitForSeconds(0.01f);
        }
        fillPaneller[2].GetComponent<Image>().fillAmount = 1;
        fillPaneller[3].GetComponent<Image>().fillAmount = 1;
        _Texts[5].enabled = true;

    }

    IEnumerator PanelKarart()
    {
        StartCoroutine(NasilOynanirFillDegistir());
        panelKontrol = true;
        paneller[0].SetActive(false);
        paneller[1].SetActive(true);
        for (float i = 0; i <= 0.5f; i += 0.01f)
        {
            paneller[1].GetComponent<Image>().color = new Vector4(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);
        }
        panelKontrol = false;

    }
    IEnumerator PanelAc()
    {
        StartCoroutine(BaslangicPanelFillDegistir());
        panelKontrol = true;
        paneller[1].SetActive(false);
        paneller[0].SetActive(true);
        for (float i = 0.5f; i >= 0; i -= 0.01f)
        {
            paneller[0].GetComponent<Image>().color = new Vector4(0, 0, 0, i);
            yield return new WaitForSeconds(0.01f);

        }

        panelKontrol = false;


    }
    public void NasilOynanirAc()
    {
        if (paneller[0].activeSelf == true && panelKontrol == false)
        {

            StartCoroutine(PanelKarart());
            _Texts[3].text = kelimeler.baslangicEkrani;


        }
        else if (panelKontrol == false)
        {

            StartCoroutine(PanelAc());
            _Texts[3].text = kelimeler.nasilOynanir;


        }
    }

    IEnumerator GecikmeliYazdir(string kelime, Text text, float harflerArasiGecikme)
    {
        for (int i = 0; i < kelime.Length; i++)
        {
            text.text += kelime[i];
            yield return new WaitForSeconds(harflerArasiGecikme);
        }
    }
}
