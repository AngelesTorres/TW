using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaspawn : MonoBehaviour
{
    public bool toma;
    public float watibom;
    public GameObject bomba;
    
    // Start is called before the first frame update
    void Start()
    {
        toma = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (toma == false)
        {
           
            watibom += Time.deltaTime;
        }

        if (watibom >=2)
        {
            toma = true;
            watibom = 0;
        }


        if (toma == true)
        {
          bomba.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="tanque" && toma == true)
        {
             bomba.SetActive(false);
            toma = false;
        }
    }

}
