using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camptura : MonoBehaviour
{

    public bool version1;
    public bool version2;
    public bool suelta;
    public int banderas;
    public GameObject demobandera;
    public Transform sueltabandera;
    public GameObject truebandera1;
    public GameObject truebandera2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (version1 == true)
        {
            
            gameObject.tag = "zonacaptura1";
        }


        if (version2 == true)
        {
           
            gameObject.tag = "zonacaptura2";
        }

        if (suelta == true && gameObject.tag == "zonacaptura1")
        {
            demobandera.SetActive(false);
            Instantiate(truebandera1, sueltabandera.position, sueltabandera.rotation);
            suelta = false;
            banderas = 0;
        }

        if (suelta == true && gameObject.tag == "zonacaptura2")
        {
            demobandera.SetActive(false);
            Instantiate(truebandera2, sueltabandera.position, sueltabandera.rotation);
            suelta = false;
            banderas = 0;
        }
    }

    public bool dejalo
    {
        set
        {
            suelta = value;
        }

    }

    public bool quien1
    {

        set
        {

             version1 = value;

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="bandera1" &&gameObject.tag == "zonacaptura1")
        {
            demobandera.SetActive(true);
            suelta = false;
            banderas = 1;
        }

        if (other.gameObject.tag == "bandera2" && gameObject.tag == "zonacaptura2")
        {
            demobandera.SetActive(true);
            suelta = false;
            banderas = 1;
        }

    }


    public bool quien2
    {

       set
        {


           version2 = value;
        }

       
    }

    public int cantidadB
    {
        get
        {
            return banderas;

        }

    }
}
