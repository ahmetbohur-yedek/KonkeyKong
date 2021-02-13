using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fici : MonoBehaviour
{
    private Rigidbody _rb;
    private bool varilSil = false;
    private Renderer _renderer;
    private Karakter _karakter;
    public float ficiHizi;
    public Material _material;
    Material[] eski_Materials;

    void Awake()
    {
        _karakter = GameObject.FindGameObjectWithTag("Player").GetComponent<Karakter>();
    }
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _renderer = this.GetComponent<Renderer>();
        eski_Materials = _renderer.materials;
        FiciMateryalTanimla();
        StartCoroutine(VarilDogdur());
        Destroy(this.gameObject, 20f);
    }
    void FixedUpdate()
    {
        if (_karakter.yasiyormu)
        {
            FiciHareketEttir();
        }
        else if (varilSil == false)
        {
            varilSil = true;
            FiciMateryalTanimla();
            StartCoroutine(VarilYokEt());
        }

    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "varilYokEt" || coll.transform.tag == "oyuncuKalkan")
        {
            StartCoroutine(VarilYokEt());

        }
    }

    IEnumerator VarilYokEt()
    {
        for (float i = -0.5f; i <= 1; i += 0.01f)
        {
            _renderer.materials[0].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
            _renderer.materials[1].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
        yield return null;
    }
    IEnumerator VarilDogdur()
    {
        for (float i = 0.5f; i >= -1; i -= 0.01f)
        {
            _renderer.materials[0].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
            _renderer.materials[1].SetFloat("Vector1_db47a61dc65c47bd8ae2f4ffd2d89f52", i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
    void FiciMateryalTanimla()
    {

        _renderer.materials = new Material[2] { _material, _material };
        _renderer.materials[0].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", eski_Materials[0].color);
        _renderer.materials[1].SetColor("Color_3ff6dc128a46448cbcbf49ca6a53c60b", eski_Materials[1].color);


    }

    void FiciHareketEttir()
    {
        _rb.AddForce(new Vector3(1, 0, 0) * ficiHizi * Time.deltaTime, ForceMode.Impulse);
    }

}
