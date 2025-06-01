using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camptura : MonoBehaviour
{

    public bool version1;
    public bool version2;
   
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
    }


    public bool quien1
    {

        set
        {

             version1 = value;

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
