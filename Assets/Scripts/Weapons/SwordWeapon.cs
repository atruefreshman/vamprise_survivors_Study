using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon
{
    public EnemyDamagerClose enemyDamager;
    private Transform sword;
    [HideInInspector] public float timeBetweenSpawn;
    [HideInInspector] public float spawnCounter;
    private float angel;
    private Transform rotation;
    private Vector3 mousePositionInWorld;
    void Start()
    {
        rotation = transform.Find("Rotation").GetComponent<Transform>();
        sword = transform.Find("Holder");
        SetStats();
    }

    void Update()
    {
        
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            if (UIController.instance.levelUpPanel.activeSelf == false) 
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 mousePositionThanPlayer = mousePositionInWorld - PlayerController.instance.transform.position;
                    float x = mousePositionThanPlayer.x;
                    float y = mousePositionThanPlayer.y;
                    spawnCounter = timeBetweenSpawn;
                    if (x > 0)
                    {
                        if ((y > 0 && x >= y) || (y < 0 && x >= -y)) angel = 90;
                        else if (y > x) angel = 180;
                        else angel = 0;
                    }
                    else
                    {
                        if ((y > 0 && -x >= y) || (y < 0 && -x >= -y)) angel = -90;
                        else if (y > -x) angel = 180;
                        else angel = 0;
                    }
                    Instantiate(sword, transform.position, Quaternion.Euler(0, 0, angel), rotation).gameObject.SetActive(true);
                    AudioController.instance.PlaySFXPiched(1);
                }
            }
        }
        rotation.rotation = Quaternion.Euler(0.0f, 0.0f, rotation.rotation.eulerAngles.z - 360 * Time.deltaTime);

        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    public void SetStats()
    {
        enemyDamager.DamageAmount = weaponStates[weaponLevel].Damage;                               //伤害
        transform.localScale = Vector3.one * weaponStates[weaponLevel].Range;                                //生成
        timeBetweenSpawn = weaponStates[weaponLevel].TimeBetweenSpawn;                                 //间隔
        enemyDamager.LifeTime = weaponStates[weaponLevel].Duration;                                          //持续时间
        /*
        enemyDamager.speed = weaponStates[weaponLevel].Speed;                                                   //速度
        amount = weaponStates[weaponLevel].Amount;                                                                     //数量
        */
    }


}
