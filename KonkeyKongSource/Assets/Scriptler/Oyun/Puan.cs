using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Puan : MonoBehaviour
{
    RaycastHit hit;
    public float skor;
    public Text[] texts;
    GameObject ficiKontrol;

    public Kelimeler kelimeler;

    void Awake()
    {
        int dil = PlayerPrefs.GetInt("Dil");
        if (dil == 0)
        {
            if (Application.systemLanguage == SystemLanguage.Turkish)
            {
                kelimeler.Turkce();
            }
            else if (Application.systemLanguage == SystemLanguage.English)
            {
                kelimeler.Ingilice();
            }
        }
        if (dil == 1)
        {
            kelimeler.Turkce();
        }
        if (dil == 2)
        {
            kelimeler.Ingilice();
        }
    }
    void Start()
    {
        skor = 0;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            texts[0].text = kelimeler.puan + "\n" + skor.ToString();
            texts[1].text = kelimeler.puan + "\n" + skor.ToString();
            texts[2].text = kelimeler.yuksekPuan + "\n" + PlayerPrefs.GetInt("YPuan").ToString();
            texts[3].text = kelimeler.yenidenOyna;
            texts[4].text = kelimeler.baslangicEkrani;
            texts[5].text = kelimeler.shiftileIsinlanabilirsin;
        }


    }
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.tag == "enemy" && hit.transform.gameObject != ficiKontrol)
            {
                PuanEkle();
                ficiKontrol = hit.transform.gameObject;
                //Debug.Log(skor);
            }
        }
    }
    public void PuanEkle()
    {
        skor++;
        texts[0].text = kelimeler.puan + "\n" + skor.ToString();
        texts[1].text = kelimeler.puan + "\n" + skor.ToString();
        if (PlayerPrefs.GetInt("YPuan") < skor)
        {
            PlayerPrefs.SetInt("YPuan", (int)skor);
            texts[2].text = kelimeler.yuksekPuan + "\n" + PlayerPrefs.GetInt("YPuan").ToString();

        }
    }



    public void SahneDegistir(int Sahne)
    {
        SceneManager.LoadScene(Sahne, LoadSceneMode.Single);
    }
}
