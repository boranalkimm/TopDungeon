using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{

// All of its functions are virtual, hence it is an abstract class, thus we write abstract in front of it and so explicitly
// state that it is abstract, therefore, as required for all virtual functioned classes, as required for abstract classes, this explicit
// statement disallows us to use this class directly on an object, so that it cannot be directly used but it can just be inherited.

// Although this is an abstract class so that all of its functions are virtual, since not all of its functions are equal to 0 so that not
// all of its functions (which are all virtual since it is an abstract class) are empty in the base, it is not an interface.
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    protected int default_facing_direction = 1;
    // 1 for right, -1 for left


    // The below are for NPCs that do not get input but use pre-calculated values for movement, and so they are protected since they will not
    // be used here but they will be used in Enemy script which will inherit from this script hence for Enemy script to be able to use these
    // we must make them protected such that they will be accessible from the children of Mover and since Enemy will inherit from Mover so that
    // Enemy will be a child of Mover it will be able to access these since they are protected so that they are still like private not accesible
    // from outside but unlike private they are accesible by the children of this class.

    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;




    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();


    }


    protected virtual void UpdateMotor(Vector3 input)
    {
// The keyword 'this' refers to the the current instance of the class, as usual.
// So it refers to that specific instance of the class, so to that specific instance of the script.
// So 'this' refers to that dragged and dropped instance of the script, so 'this' refers to the current script instance.
// 'GameObject' is a type keyword that means any GameObject, so every object in the Unity is of type GameObject and that is
// the typename for it. But, with somewhat a misuse of notation, camelcase version of it, gameObject, refers to our current GameObject,
// so,
// ****  gameObject = current GameObject ****
// More clearly, current GameObject, meaning gameObject, means the current GameObject that is attached to the current instance of our script.
// Therefore, it is the GameObject that is attached to 'this', meaning it is the GameObject that is attached to the current instance of our script.
// Also, writing nothing in front of something, like writing transform.position, implicitly means current GameObject's transform.position, so it
// means gameObject's transform.position, so it means, since 'this' is implicitly before everything, this's gameObject's transform.position
// therefore 
// **** transform.position is an abbreviation for this.gameObject.transform.position *****
// gameObject = current GameObject
// this.gameObject = current GameObject of current instance of the script
// Do not forget these.

        if (this.gameObject.tag == "Fighter" && this.gameObject.name != "Player" && this.gameObject.name != "IntroNPC" && this.gameObject.name != "Twin")
        {
            default_facing_direction = -1;
        }




        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);




        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * default_facing_direction, transform.localScale.y, transform.localScale.z);
        }

        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (-default_facing_direction), transform.localScale.y, transform.localScale.z);
        }


        moveDelta = moveDelta + pushDirection;

        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));


        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }


        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));


        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

    }




    
}
