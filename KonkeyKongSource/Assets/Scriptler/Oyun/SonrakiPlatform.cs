using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonrakiPlatform : MonoBehaviour
{
    public GameObject platform;
    public GameObject olusanPlatform;

    void Start()
    {
        PlatformOlustur();
    }
    IEnumerator BasamakYukselt()
    {
        if (olusanPlatform.activeSelf == false)
        {
            olusanPlatform.SetActive(true);
            for (int i = 0; i < 100; i++)
            {
                olusanPlatform.transform.position += new Vector3(0, 0.01f, 0);
                yield return new WaitForSecondsRealtime(0.01f);

            }
            olusanPlatform.transform.position = new Vector3(0, this.transform.position.y + 3, this.transform.position.z + 7);

        }
    }
    void PlatformOlustur()
    {
        olusanPlatform = Instantiate(platform, new Vector3(0, this.transform.position.y + 2, this.transform.position.z + 7), Quaternion.identity);
        olusanPlatform.SetActive(false);
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
