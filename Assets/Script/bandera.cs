using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandera : MonoBehaviour
{
    
    public Transform casa;
 
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
        if (bandera1 == true && other.gameObject.tag=="zonacaptura1")
        {

            transform.position = casa.position;
            
        }

        if (bandera2 == true && other.gameObject.tag == "zonacaptura2")
        {
            transform.position = casa.position;

        }


    }

    

}
