using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;


    protected float immuneTime = 1.0f;
    public float lastImmune;

    public Vector3 pushDirection;


    protected virtual void ReceiveDamage(Damage dmg)
    {

        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint = hitpoint - dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }




    }

    protected virtual void Death()
    {

    }

 
}
