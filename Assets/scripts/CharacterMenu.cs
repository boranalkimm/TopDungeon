using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;




    private void Start()
    {
        if (GameManager.menu_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.menu_instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void OnArrowClick(bool right)
    {

        if (right)
        {

            currentCharacterSelection = currentCharacterSelection + 1;

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {

                currentCharacterSelection = 0;


            }


            OnSelectionChanged();

        }


        else
        {

            currentCharacterSelection = currentCharacterSelection - 1;


            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }


            OnSelectionChanged();


        }


    }




    private void OnSelectionChanged()
    {


        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);

    }



    public void OnUpgradeClick()
    {

        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }

    }


    public void UpdateMenu()
    {



        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        if (GameManager.instance.weapon.weaponLevel >= GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";

        }
        else
        {
        upgradeCostText.text = (GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel]).ToString();
        }





        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();




        if(GameManager.instance.GetCurrentLevel() == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }

        else
        {
            int max_xp_of_current_level = GameManager.instance.GetXpToLevel(GameManager.instance.GetCurrentLevel() + 1);
            int min_xp_of_current_level = GameManager.instance.GetXpToLevel(GameManager.instance.GetCurrentLevel());
            int diff = max_xp_of_current_level - min_xp_of_current_level;
            int current_xp_into_level = GameManager.instance.experience - min_xp_of_current_level;
            float ratio = (float)current_xp_into_level / diff;
            xpBar.localScale = new Vector3(ratio, 1, 1);
            xpText.text = GameManager.instance.experience.ToString() + " / " + max_xp_of_current_level;
        }







    }




}
