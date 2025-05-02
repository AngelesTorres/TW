using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class nucleo : NetworkBehaviour
{
    public bool recibe;
    public float live;
    public float waitda�o;
    public Material colorda�o;   

    public override void Spawned()
    {
        recibe = false;
        live = 3;
        colorda�o.color = Color.green;
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
          
            colorda�o.color = Color.red;
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
