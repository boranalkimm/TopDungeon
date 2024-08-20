using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{



    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetBool("show"))
        {
            Time.timeScale = 0;
        }

        if (anim.GetBool("hide"))
        {
            Time.timeScale = 1;
        }





        
    }
}
