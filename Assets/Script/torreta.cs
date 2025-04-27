using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class torreta : MonoBehaviour
{
    public float rotationSpeed = 100f;
    [SerializeField]
    public GameObject prefbal;
    public Transform disparo;


    void Update()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.Q))
            input = -1f;
        else if (Input.GetKey(KeyCode.E))
            input = 1f;

       
        transform.Rotate(Vector3.up * input * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefbal, disparo.position, disparo.rotation);
        }

    }
}
