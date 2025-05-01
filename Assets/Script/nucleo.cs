using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class nucleo : NetworkBehaviour
{
    public bool recibe;
    public float live;
    public float waitdaño;
    public Material colordaño;
    
    void Start()
    {
        recibe = false;
        live = 3;
        colordaño.color = Color.green;
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;

      

        if (live <=0)
        {
           Destroy(gameObject);
            print("muerto");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="bala" )
        {
            live = live - 0.5f;
          
            colordaño.color = Color.red;
        }
    }
}
