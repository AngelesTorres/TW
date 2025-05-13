using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desactivadaño : MonoBehaviour
{
    public Player Player;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="tanque")
        {
            Player.GetComponent<Player>().otrabala = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "tanque")
        {
            Player.GetComponent<Player>().otrabala = false;
        }
    }
}
