using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class murorompible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag=="bomba")
        {            
            Destroy(gameObject);
        }              
    }
}
