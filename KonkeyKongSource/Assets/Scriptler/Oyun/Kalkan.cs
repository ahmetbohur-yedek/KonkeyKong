using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kalkan : MonoBehaviour
{
    private GameObject karakter;
    private Renderer _renderer;

    [ColorUsageAttribute(true, true)]
    public Color[] renkler;
    public Color randomRenk;
    public bool kalkanDurumu;
    void Awake()
    {
        karakter = GameObject.FindGameObjectWithTag("Player");
        _renderer = this.GetComponent<Renderer>();
        randomRenk = renkler[Random.Range(0, renkler.Length)];
        _renderer.material.SetColor("Color_8c788506be1d4252add11f2d7aec4108", randomRenk);
    }

    void FixedUpdate()
    {
        KarakterTakip();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "enemy")
        {
            karakter.GetComponent<Puan>().PuanEkle();
            //Debug.Log("kalkan point");
        }
    }

    IEnumerator KalkanAc()
    {
        if (kalkanDurumu != true)
        {
            kalkanDurumu = true;
            this.GetComponent<SphereCollider>().enabled = true;
            for (float i = +1f; i >= -1; i -= 0.01f)
            {
                _renderer.material.SetFloat("Vector1_b143930b166949f08cb54d832bd5caa6", i);
                yield return new WaitForSeconds(0.01f);
            }
        }

    }
    IEnumerator KalkanKapat()
    {

        for (float i = -1f; i <= +2; i += 0.01f)
        {
            _renderer.material.SetFloat("Vector1_b143930b166949f08cb54d832bd5caa6", i);
            yield return new WaitForSeconds(0.01f);
        }
        this.GetComponent<SphereCollider>().enabled = false;
        karakter.gameObject.layer = 8;
        randomRenk = renkler[Random.Range(0, renkler.Length)];
        _renderer.material.SetColor("Color_8c788506be1d4252add11f2d7aec4108", randomRenk);
        kalkanDurumu = false;

    }
    IEnumerator KalkanKapatmaAnimasyonu()
    {
        for (float i = -1f; i <= +2; i += 0.01f)
        {
            _renderer.material.SetFloat("Vector1_b143930b166949f08cb54d832bd5caa6", i);
            yield return new WaitForSeconds(0.01f);
        }
        this.GetComponent<SphereCollider>().enabled = false;
        randomRenk = renkler[Random.Range(0, renkler.Length)];
        _renderer.material.SetColor("Color_8c788506be1d4252add11f2d7aec4108", randomRenk);
        kalkanDurumu = false;
    }

    public void KalkanAcarmisin()
    {
        StartCoroutine(KalkanAc());
    }
    public void KalkanKapatirmisin()
    {
        StartCoroutine(KalkanKapat());
    }
    public void KalkanKapatirmisinLayerOlmadan()
    {
        StartCoroutine(KalkanKapatmaAnimasyonu());
    }
    void KarakterTakip()
    {
        this.transform.position = karakter.transform.position + new Vector3(0, 0.5f, 0);
    }
}
