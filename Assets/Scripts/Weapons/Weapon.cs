using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public List<WeaponStates> weaponStates;
    [HideInInspector] public int weaponLevel;
    protected bool statsUpdated;
    public Sprite weaponIcon;

    public void LevelUp() 
    {
        if(weaponLevel<weaponStates.Count-1)
            weaponLevel++;
        statsUpdated = true;
        if (weaponLevel >= weaponStates.Count - 1) 
        {
            PlayerController.instance.fullyLevelledWeapons.Add(this);
            PlayerController.instance.assigneWeapons.Remove(this);
        }
    }
}

[Serializable]
public class WeaponStates 
{
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }
    [SerializeField] private float damage;
    public float Damage {  get { return damage; } }
    [SerializeField] private float range;
    public float Range {  get { return range; } }
    [SerializeField] private float timeBetweenSpawn;
    public float TimeBetweenSpawn { get { return timeBetweenSpawn; } }
    [SerializeField] private int amount;
    public int Amount { get { return amount; } }
    [SerializeField] private float duration;
    public float Duration { get { return duration; } }
    [SerializeField] private float timeBetweenDamage;
    public float TimeBetweenDamage { get {  return timeBetweenDamage; } }
    [SerializeField] private string upgradeDescription;
    public string UpgradeDescription { get {  return upgradeDescription; } }
}
