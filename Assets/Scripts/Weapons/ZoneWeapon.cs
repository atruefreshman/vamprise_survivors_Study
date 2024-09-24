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
        enemyDamager.DamageAmount = weaponStates[weaponLevel].Damage;   //�˺�
        enemyDamager.transform.localScale = Vector3.one * weaponStates[weaponLevel].Range;      //��С
        timeBetweenSpawn = weaponStates[weaponLevel].TimeBetweenSpawn;    //���ɼ��
        enemyDamager.LifeTime = weaponStates[weaponLevel].Duration;              //����ʱ��
        enemyDamager.timeBetweenDamage = weaponStates[weaponLevel].TimeBetweenDamage;                   //�˺����ʱ��
    }
}
