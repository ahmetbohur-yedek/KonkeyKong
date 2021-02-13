using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaslangicKarakter : MonoBehaviour
{
    // Start is called before the first frame update    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "oyunuBaslat")
        {
            this.GetComponent<Karakter>().ziplamaGucu = 0;
            StartCoroutine(this.GetComponent<Karakter>().OlumEfekti(0));
            StartCoroutine(OyunuBaslat());
        }
        if (coll.transform.tag == "oyunuKapat")
        {
            this.GetComponent<Karakter>().ziplamaGucu = 0;
            StartCoroutine(this.GetComponent<Karakter>().OlumEfekti(0));
            StartCoroutine(OyunuKapat());
        }
    }
    IEnumerator OyunuKapat()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
    IEnumerator OyunuBaslat()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void SahneDegistir(int Sahne){
        SceneManager.LoadScene(Sahne, LoadSceneMode.Single);
    }
}
