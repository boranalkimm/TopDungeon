using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    public static Player player_instance;
    public static FloatingTextManager float_instance;
    public static CharacterMenu menu_instance;
    public static Hud hud_instance;
    public static DeathHud deathHud_instance;
    public bool bossAlive;

    private void Awake()
    {

        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        bossAlive = true;
        DontDestroyOnLoad(gameObject);
    }



    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Weapon weapon;

    public Player player;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public RectTransform maxHitpointBar;
    public Animator deathMenuAnim;


    public int pesos;
    public int experience;



    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
        
    }




    public bool TryUpgradeWeapon()
    {
        if (weapon.weaponLevel >= weaponPrices.Count)
        {
            return false;
        }
        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos = pesos - weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }









    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }


    public void OnMaxHitpointChange()
    {
        maxHitpointBar.sizeDelta = maxHitpointBar.sizeDelta + new Vector2(0, 20);
        hitpointBar.anchoredPosition = Vector3.zero;
    }






















    public int GetCurrentLevel()
    {

        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            if (r == xpTable.Count)
            {
                return r;
            }
            add = add + xpTable[r];
            r = r + 1;

        }

        return r - 1;


    }


    public int GetXpToLevel(int level)
    {

        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp = xp + xpTable[r];
            r = r + 1;
        }


        return xp;


    }



    public void GrantXp(int xp)
    {
        int level_before_grant = GetCurrentLevel();
        experience = experience + xp;
        int level_after_grant = GetCurrentLevel();
        if (level_before_grant != level_after_grant)
        {

            int level_difference = level_after_grant - level_before_grant;
            for (int j = 0; j < level_difference; j = j + 1)
            {
                OnLevelUp();
            }
            
        }

    }

    public void OnLevelUp()
    {
        player.LeveledUp();
    }





    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        player.lastImmune = Time.time;
        player.pushDirection = Vector3.zero;

    }









    public void SaveState()
    {

        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);

    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {


        SceneManager.sceneLoaded -= LoadState;

        if (!(PlayerPrefs.HasKey("SaveState")))
        {

            return;

        }

        string[] data = (PlayerPrefs.GetString("SaveState")).Split('|');

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.SetWeaponLevel(int.Parse(data[3]));


        player.SetLevel(GetCurrentLevel());



    }




    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
        weapon.SetWeaponLevel(0);
        pesos = 0;
        experience = 0;
    }
   



}
