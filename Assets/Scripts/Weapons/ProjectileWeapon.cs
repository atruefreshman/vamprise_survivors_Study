using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamagerOnProjectile enemyDamager;
    private float timeBetweenSpawn;
    private float spawnCounter;
    private int amount;
    public LayerMask whatIsEnemy;

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
            StartCoroutine(Spawn());
        }

        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    public void SetStats() 
    {
        enemyDamager.speed = weaponStates[weaponLevel].Speed;                                                   //速度
        enemyDamager.DamageAmount = weaponStates[weaponLevel].Damage;                               //伤害
        amount = weaponStates[weaponLevel]. Amount;                                                                     //数量
        enemyDamager.transform.localScale = Vector3.one * weaponStates[weaponLevel].Range;                                //大小
        timeBetweenSpawn = weaponStates[weaponLevel].TimeBetweenSpawn;                                 //射击间隔
        enemyDamager.LifeTime = weaponStates[weaponLevel].Duration;                                          //飞行时间
        enemyDamager.penetrationNumber = weaponStates[weaponLevel].TimeBetweenDamage;    //穿透数
    }

    IEnumerator Spawn() 
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 6.8f, whatIsEnemy);
        if (enemies.Length > 0)
        {
            int minIndex = 0;
            float min = 10.0f;
            for (int i = 0; i < enemies.Length; i++)
            {
                float cur = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (cur < min)
                {
                    minIndex = i;
                    min = cur;
                }
            }
            Vector3 direction = enemies[minIndex].transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            AudioController.instance.PlaySFXPiched(2);
            //GameObject projectile;
            if (amount == 1) 
            {
                /*projectile= ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation= Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
            }
            else if (amount == 2)
            {
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
            }
            else if (amount == 3)
            {
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle + 25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle + 25, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle - 25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle - 25, Vector3.forward)).gameObject.SetActive(true);
            }
            else if (amount == 4)
            {
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle+25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle + 25, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle - 25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle - 25, Vector3.forward)).gameObject.SetActive(true);
            }
            else 
            {
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle + 25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle + 25, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle - 25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle - 25, Vector3.forward)).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.25f);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle + 25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle + 25, Vector3.forward)).gameObject.SetActive(true);
                /*projectile = ObjectPool.Instance.GetObject(enemyDamager.gameObject);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.AngleAxis(angle - 25, Vector3.forward);*/
                Instantiate(enemyDamager.gameObject, transform.position, Quaternion.AngleAxis(angle - 25, Vector3.forward)).gameObject.SetActive(true);
            }
            AudioController.instance.PlaySFXPiched(2);
        }
    }

}
