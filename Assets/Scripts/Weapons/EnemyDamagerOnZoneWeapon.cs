using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagerOnZoneWeapon : EnemyDamager
{
    [HideInInspector] public float timeBetweenDamage;
    //public float TimeBetweenDamage { set { timeBetweenDamage = value; } }
    private float damageCounter;
    private List<EnemyController> enemyInRange;
    protected override void  Start()
    {
        base.Start();
        enemyInRange = new List<EnemyController>();
        damageCounter = timeBetweenDamage;
    }

    protected override void Update()
    {
        base.Update();
        damageCounter -= Time.deltaTime;
        if (damageCounter < 0)
        {
            damageCounter = timeBetweenDamage;
            for (int i = 0; i < enemyInRange.Count; i++)
                if (enemyInRange[i] != null)
                    enemyInRange[i].TakeDamage(damageAmount, shouldKonckBack);
                else
                {
                    enemyInRange.RemoveAt(i);
                    i--;
                }
        }

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > 3.0f)
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, PlayerController.instance.moveSpeed * 1.2f * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, PlayerController.instance.moveSpeed * 0.4f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
            enemyInRange.Add(other.GetComponent<EnemyController>());
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
            enemyInRange.Remove(other.GetComponent<EnemyController>());
    }
}
