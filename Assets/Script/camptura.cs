using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camptura : MonoBehaviour
{

    public bool version1;
    public bool version2;
    public bool suelta;
    public GameObject demobandera;
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

        if (suelta == true )
        {
            demobandera.SetActive(false);
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
        }

        if (other.gameObject.tag == "bandera2" && gameObject.tag == "zonacaptura2")
        {
            demobandera.SetActive(true);
            suelta = false;
        }

    }


    public bool quien2
    {

       set
        {


           version2 = value;
        }

       
    }

}
