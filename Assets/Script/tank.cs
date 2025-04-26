using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;

    private Rigidbody rb;

    public GameObject demobomb;
    public GameObject demobomb2;
    public GameObject demobomb3;

    public float countbomb;
    public bool espera;
    public float waitmore;

    public GameObject bomba;
    public Transform bomsalida;
    public float nomas;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        espera = true;
    }


    private void Update()
    {
        if (countbomb >= 1)
        {
            demobomb.SetActive(true);
           
        }
        else
        {
            demobomb.SetActive(false);
        }


        if (countbomb >= 2)
        {
            demobomb2.SetActive(true);
            
        }
        else
        {
            demobomb2.SetActive(false);
        }


        if (countbomb == 3)
        {
            demobomb3.SetActive(true);
           
        }
        else
        {
            demobomb3.SetActive(false);
        }


        if (espera == false)
        {
            waitmore += Time.deltaTime;
        }


        if (waitmore >= 2)
        {
            espera = true;
            
        }


        if (Input.GetKeyDown(KeyCode.B) && countbomb>=1)
        {
            Instantiate(bomba, bomsalida.position, bomsalida.rotation);
            countbomb = countbomb - 1;
        }
    }


    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        Vector3 moveDirection = transform.forward * move * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn * Time.fixedDeltaTime, 0f);

        rb.MovePosition(rb.position + moveDirection);
        rb.MoveRotation(rb.rotation * turnRotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="bombspawn" && espera == true )
        {
            countbomb = countbomb + 1;
            espera = false;
        }
    }
}
