using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : Weapon
{
    private float timeBetweenSpawn;
    private float spawnCounter;
    public EnemyDamagerThrow enemyDamager;
    private int amount;
    void Start()
    {
        SetStats();
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            for (int i = 1; i <= amount; i++) 
            {
                Instantiate(enemyDamager, transform.position, Quaternion.identity).gameObject.SetActive(true);
                AudioController.instance.PlaySFXPiched(3);
            }
            spawnCounter = timeBetweenSpawn;
        }

        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    public void SetStats()
    {
        enemyDamager.DamageAmount = weaponStates[weaponLevel].Damage;                               //伤害
        enemyDamager.transform.localScale = Vector3.one * weaponStates[weaponLevel].Range;                                //大小
        timeBetweenSpawn = weaponStates[weaponLevel].TimeBetweenSpawn;                                 //间隔
        enemyDamager.LifeTime = weaponStates[weaponLevel].Duration;                                          //持续时间
        amount = weaponStates[weaponLevel].Amount;                                                                     //数量
    }
}
