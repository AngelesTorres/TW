using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class murorompible : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="bomba")
        {            
            Destroy(gameObject);
        }       
    }
}
