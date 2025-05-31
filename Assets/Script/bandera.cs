using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandera : MonoBehaviour
{
    
    public Transform casa;
    public Player tanque;
    public bool bandera1;
    public bool bandera2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bandera1 == true)
        {
            if (other.gameObject.tag=="tanque" )
            {
                Destroy(gameObject);
            }
        }

        if (bandera2 == true)
        {
            if (other.gameObject.tag == "tanque" )
            {
                Destroy(gameObject);
            }
        }


    }

    

}
