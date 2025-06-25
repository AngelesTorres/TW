using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaspawn : MonoBehaviour
{
    public bool toma;
    public float watibom;
    public GameObject bomba;
    
    void Start()
    {
        toma = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="tanque" && toma == true)
        {
             bomba.SetActive(false);
            toma = false;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);

        toma = true;
        bomba.SetActive(true);
    }
}
