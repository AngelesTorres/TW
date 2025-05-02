using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador : MonoBehaviour
{
  public nucleo nucle;

    public Player tank;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (nucle.nucleolive <=0)
        {
            print("hola");
        }

       
  

    }

   

}
