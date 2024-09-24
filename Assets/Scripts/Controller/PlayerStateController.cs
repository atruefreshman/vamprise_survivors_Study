using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public static PlayerStateController instance;
    private void Awake()
    {
        instance = this;
    }

    public List<PlayerStataValue> moveSpeed, health, pickupRange, maxWeapons;
    private int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;

    public void UpdateDisplay() 
    {
        if (moveSpeedLevel < moveSpeed.Count - 1)
            UIController.instance.moveSpeed.UpdateDispaly(moveSpeed[moveSpeedLevel].Cost, moveSpeedLevel);
        else    UIController.instance.moveSpeed.MaxLevel();
        if(healthLevel<health.Count-1)
            UIController.instance.health.UpdateDispaly(health[healthLevel].Cost,healthLevel);
        else   UIController.instance.health.MaxLevel();
        if(pickupRangeLevel<pickupRange.Count-1)
           UIController.instance.pickupRange.UpdateDispaly(pickupRange[pickupRangeLevel].Cost, pickupRangeLevel);
        else   UIController.instance.pickupRange.MaxLevel();
        if(maxWeaponsLevel<maxWeapons.Count-1)
            UIController.instance.maxWeapon.UpdateDispaly(maxWeapons[maxWeaponsLevel].Cost,maxWeaponsLevel);
        else UIController.instance.maxWeapon.MaxLevel();
    }

    public void PurchaseMoveSpeed() 
    {
        CoinController.instance.AddCoins(-moveSpeed[moveSpeedLevel].Cost);
        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].Value;
        moveSpeedLevel++;
        UpdateDisplay();
    }
    public void Purchasehealth() 
    {
        float maxHealth = PlayerHealthController.instance.maxHealth;     
        CoinController.instance.AddCoins(-health[healthLevel].Cost);
        PlayerHealthController.instance.maxHealth = health[healthLevel].Value;
        if (healthLevel == 0)
            PlayerHealthController.instance.curHealth += (health[healthLevel].Value - maxHealth);
        else
            PlayerHealthController.instance.curHealth += (health[healthLevel].Value- health[healthLevel-1].Value);
        healthLevel++;
        UpdateDisplay();
    }
    public void PurchasepickupRange() 
    {
        CoinController.instance.AddCoins(-pickupRange[pickupRangeLevel].Cost);
        PlayerController.instance.circleCollider.radius = pickupRange[pickupRangeLevel].Value;
        pickupRangeLevel++;
        UpdateDisplay();
    }
    public void PurchaseMaxWeapons() 
    {
        CoinController.instance.AddCoins(-maxWeapons[maxWeaponsLevel].Cost);
        PlayerController.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].Value);
        maxWeaponsLevel++;
        UpdateDisplay();
    }
}

[Serializable]
public class PlayerStataValue 
{
    [SerializeField]private int cost;
    public int Cost { get { return cost; } }
    [SerializeField]private float value;
    public float Value { get { return value; } }
}
