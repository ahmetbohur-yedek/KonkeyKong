using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuTakip : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _Player;
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
      
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player.transform.position.y > 5)
        {
            this.transform.position = new Vector3(0, this.transform.position.y, _Player.transform.position.z);
            this.transform.Translate(0, 0.25f * Time.deltaTime, 0);
        }
        else if (_Player.transform.position.y < 5)
        {
            this.transform.position = new Vector3(0, this.transform.position.y, _Player.transform.position.z);
            this.transform.Translate(0, 0.1f * Time.deltaTime, 0);
        }

    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag != "Player")
        {
            Destroy(collider.transform.gameObject);
        }else{
            collider.GetComponent<Karakter>().KarakterOldur();
        }
    }
}
