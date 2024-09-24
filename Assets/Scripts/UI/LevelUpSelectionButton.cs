using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Image weaponIcon;

    private Weapon assignedWeapon;
 /*   void Awake()
    {
        nameText = transform.Find("NameText").GetComponent<TMP_Text>();
        descriptionText= transform.Find("UpgradeDescriptionText").GetComponent<TMP_Text>();
        weaponIcon=transform.Find("Image").GetComponent<Image>();
    }
 */
    public void UpdateButtonDisplay(Weapon weapon) 
    {
        if (weapon.gameObject.activeSelf)
        {
            descriptionText.text = weapon.weaponStates[weapon.weaponLevel].UpgradeDescription;
            weaponIcon.sprite = weapon.weaponIcon;
            nameText.text = weapon.name + " - level " + weapon.weaponLevel;
        }
        else
        {
            descriptionText.text = "unlock ";
            weaponIcon.sprite = weapon.weaponIcon;
            nameText.text = weapon.name;
        }
        assignedWeapon = weapon;
    }


    public void SelectUpgrade() 
    {
        if (assignedWeapon != null) 
        {
            if(assignedWeapon.gameObject.activeSelf)
                assignedWeapon.LevelUp();
            else
                PlayerController.instance.AddWeapon(assignedWeapon);
            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale=1.0f;
        }
    }
}
