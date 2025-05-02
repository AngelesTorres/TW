using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador : MonoBehaviour
{
  public nucleo nucle;

    public Player tank;
   
    void Start()
    {
        
    }

    void Update()
    {
        if (nucle.nucleolive <=0)
        {
            print("hola");
        }         
    }   
}
