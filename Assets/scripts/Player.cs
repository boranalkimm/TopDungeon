using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{


    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;




    protected override void Start()
    {
        if (GameManager.player_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        GameManager.player_instance = this;
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject);
    }


    protected override void ReceiveDamage(Damage dmg)
    {

        if (!isAlive)
        {
            return;
        }



        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();

    }






    private void FixedUpdate()
    {
        

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        if (isAlive)
        {

        UpdateMotor(new Vector3(x, y, 0));
        
        }




    }



    // GetComponent action is a costly action, so, if there is a way to avoid duplicate
    // GetComponent actions, you should always do that and avoid unnecessary GetComponents for
    // efficiency reasons.



    public void SwapSprite(int skinIndex)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinIndex];
    }
    // Although our sprites change, our spriteRenderer does not change, hence, we create a spriteRenderer variable
    // since it does not change, initialize it once at the Start(), and use the same renderer always since it does not change,
    // this way, we avoid duplicate GetComponents which is a costly action, and just take the sprite of the same spriteRenderer
    // every time since the renderer does not change, so renderer component is GetComponented only once, which
    // greatly increases efficiency.


    public void LeveledUp()
    {
        maxHitPoint = maxHitPoint + 1;
        GameManager.instance.OnMaxHitpointChange();
        hitpoint = maxHitPoint;
        GameManager.instance.OnHitpointChange();
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i = i + 1)
        {
            LeveledUp();
        }

    }


    public void Heal(int healingAmount)
    {

        if (hitpoint == maxHitPoint)
        {
            return;
        }

        if (hitpoint + healingAmount >= maxHitPoint)
        {
            GameManager.instance.ShowText("+" + (maxHitPoint - hitpoint) + " hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
            hitpoint = maxHitPoint;

        }
        else
        {
            hitpoint = hitpoint + healingAmount;
            GameManager.instance.ShowText("+" + healingAmount + " hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        }


        GameManager.instance.OnHitpointChange();





    }


    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }



    public void Respawn()
    {
        isAlive = true;
        maxHitPoint = 10;
        Heal(maxHitPoint);
        lastImmune = Time.time;
        pushDirection = Vector3.zero;

    }







}
