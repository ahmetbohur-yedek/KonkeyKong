using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiciOlusturucu : MonoBehaviour
{
    public GameObject fici;
    public float ficiHizi;
    public float ficiOlusmaSuresi;
    private float ficiOlusmaSuresiX;
    private Puan _puan;

    void Start()
    {
        _puan = GameObject.FindGameObjectWithTag("Player").GetComponent<Puan>();
        float hizlandir = (_puan.skor / 10) * ficiHizi;
        ficiHizi += hizlandir;
        //Debug.Log(ficiHizi);
        ficiOlusmaSuresi = 3 - (_puan.skor / 1000);
    }
    void Update()
    {
        if (ficiOlusmaSuresiX < 0)
        {
            FiciOlustur();
            ficiOlusmaSuresiX = ficiOlusmaSuresi;
        }
        else
        {
            ficiOlusmaSuresiX -= Time.deltaTime;
        }
    }

    void FiciOlustur()
    {
        var x = Instantiate(fici, this.transform.position, Quaternion.identity);
        x.GetComponent<Fici>().ficiHizi = ficiHizi;
    }
}
