using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : Collidable
{


    public string message;

    private float cooldown = 4.0f;
    private float lastShout = -20.0f;


    protected override void OnCollide(Collider2D coll)
    {


        if (coll.name == "Player")
        {


            if (Time.time - lastShout > cooldown)
            {
                lastShout = Time.time;
                GameManager.instance.ShowText(message, 35, Color.yellow, transform.position + new Vector3(0, 0.20f, 0), Vector3.zero, cooldown);


            }




        }



    }
    




}
