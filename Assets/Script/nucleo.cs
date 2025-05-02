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

    public override void Spawned()
    {
        recibe = false;
        live = 3;
        colordaño.color = Color.green;
        GameManager.Instance.AddToTowerList(this);
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="bala" )
        {
            live = live - 0.5f;
          
            colordaño.color = Color.red;
        }
    }

  public float nucleolive
    {
        get
        {
            return live;
        }
    }
}
