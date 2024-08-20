using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{

    public string our_sceneName;


    protected override void OnCollide(Collider2D coll)
    {

        if (coll.name == "Player")
        {
            GameManager.instance.SaveState();

            string sceneName = our_sceneName;
            SceneManager.LoadScene(sceneName);

        }

    }
    




}
