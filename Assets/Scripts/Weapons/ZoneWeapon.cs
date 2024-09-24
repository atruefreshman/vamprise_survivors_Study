using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{

    public EnemyDamagerOnZoneWeapon enemyDamager;
    private float timeBetweenSpawn;
    private float spawnCounter;
    void Start()
    {
        SetStats();
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;
            Instantiate(enemyDamager.gameObject,transform.position,Quaternion.identity).gameObject.SetActive(true);
        }

        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }
    public void SetStats()
    {
        enemyDamager.DamageAmount = weaponStates[weaponLevel].Damage;   //伤害
        enemyDamager.transform.localScale = Vector3.one * weaponStates[weaponLevel].Range;      //大小
        timeBetweenSpawn = weaponStates[weaponLevel].TimeBetweenSpawn;    //生成间隔
        enemyDamager.LifeTime = weaponStates[weaponLevel].Duration;              //持续时间
        enemyDamager.timeBetweenDamage = weaponStates[weaponLevel].TimeBetweenDamage;                   //伤害间隔时间
    }
}
