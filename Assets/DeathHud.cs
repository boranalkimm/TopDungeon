using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHud : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.deathHud_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.deathHud_instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
