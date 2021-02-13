using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basamak : MonoBehaviour
{
    public GameObject basamak;

    IEnumerator BasamakYukselt()
    {
        if (basamak.activeSelf == false)
        {
            basamak.SetActive(true);
            for (int i = 0; i < 100; i++)
            {
                basamak.transform.position += new Vector3(0, 0.01f, 0);
                yield return new WaitForSecondsRealtime(0.01f);
            
            }

        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "Player")
        {
            StartCoroutine(BasamakYukselt());
            // Debug.Log("Oyuncu ile temas edildi");
        }
    }
}
