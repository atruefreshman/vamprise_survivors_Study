using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerStateUpgradeDisplay : MonoBehaviour
{
    private TMP_Text levelText;
    private TMP_Text costText;
    GameObject button;

    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<TMP_Text>();
        costText = transform.Find("CostText").GetComponent<TMP_Text>();
        button = transform.Find("PurchaseButton").gameObject;
    }

    public void UpdateDispaly(int cost,int level)
    {
        levelText.text = "Level : "+level+"-> "+(level+1);
        costText.text = "Coin :      " +cost;
        if(cost<=CoinController.instance.CoinAmount)
            button.SetActive(true);
        else
            button.SetActive(false);
    }

    public void MaxLevel() 
    {
        levelText.text = "     Max Level";
        costText.text = "";
        button.SetActive(false);
    }
}
