using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nucleo : MonoBehaviour
{

    public bool recibe;
    public float live;
    public float waitdaño;
    
    // Start is called before the first frame update
    void Start()
    {
        recibe = true;
        live = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (recibe == false)
        {
            waitdaño += Time.deltaTime;
        }

        if (waitdaño >= 4)
        {
            recibe = true;
            waitdaño = 0;
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
