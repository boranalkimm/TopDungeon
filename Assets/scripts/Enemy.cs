using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{

    public int xpValue = 1;

    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];



    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = (transform.GetChild(0)).GetComponent<BoxCollider2D>();

    }

    private void FixedUpdate()
    {



        collidingWithPlayer = false;

        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i = i + 1)
        {
            if (hits[i] == null)
            {
                continue;
            }




            if (hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }


            hits[i] = null;

        }




        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {

            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true;
            }

            if (chasing)
            {

                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }

            }

            else
            {

                if ((startingPosition - transform.position).magnitude < 0.001)
                {

                }
                else
                {
                    UpdateMotor(startingPosition - transform.position);

                }
            }

        }

        else
        {  
            if ((startingPosition - transform.position).magnitude < 0.001)
            {

            }
            else
            {

                UpdateMotor(startingPosition - transform.position);

            }
            chasing = false;
        }




    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        (GameManager.instance).ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);

    }



    



   
}
