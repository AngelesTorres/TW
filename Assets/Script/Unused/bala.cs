using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public float velocidad = 10f;
    public float tiempoVida = 1.8f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="tanque")
        {
           Destroy(gameObject);
        }
        if (other.gameObject.tag == "nucleo")
        {
            Destroy(gameObject);
        }
    }
}
