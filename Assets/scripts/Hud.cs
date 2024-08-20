using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{

    private void Start()
    {
        if (GameManager.hud_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.hud_instance = this;
        DontDestroyOnLoad(gameObject);
    }


}
