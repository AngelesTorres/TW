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
        recibe = true;
        live = 3;
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;

        if (recibe == false)
        {
            waitdaño += Time.deltaTime;
            colordaño.color = Color.red;
        }

        if (waitdaño >= 4)
        {
            recibe = true;
            waitdaño = 0;
            colordaño.color = Color.green;
        }

        if (live <=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="bala" && recibe == true)
        {
            live = live - 1;
            recibe = false;
        }
    }
}
