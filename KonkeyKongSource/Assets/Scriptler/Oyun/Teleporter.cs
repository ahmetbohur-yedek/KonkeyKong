using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject[] katmanlar;
    public SonrakiPlatform sonrakiPlatform;

    void FixedUpdate()
    {
        if (katmanlar[0].gameObject.activeSelf == false || katmanlar[1].gameObject.activeSelf == false || katmanlar[2].gameObject.activeSelf == false || sonrakiPlatform.olusanPlatform.gameObject.activeSelf == false)
        {
            this.transform.tag = "none";
        }
        else
        {
            this.transform.tag = "teleport";
        }
    }
}
