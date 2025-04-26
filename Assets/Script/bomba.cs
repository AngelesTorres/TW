using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{

    public bool explot;
    public Animator buum;
   public  float waitb ;
    
    
    // Start is called before the first frame update
    void Start()
    {
        explot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (explot == true)
        {
            waitb += Time.deltaTime;
            if (waitb >= 3F)
            {
              buum.Play("buum");
            }

            waitb += Time.deltaTime;
            if (waitb >= 3.5F)
            {
                Destroy(gameObject);
            }
        }
    }

   

}
