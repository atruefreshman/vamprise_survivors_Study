using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagerOnProjectile : EnemyDamager
{
    [HideInInspector] public float speed;
    [HideInInspector] public float penetrationNumber;  //´©Í¸Êý
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Enemy") 
        {
            other.GetComponent<EnemyController>().TakeDamage(damageAmount,shouldKonckBack);
            penetrationNumber -= 1;
            if (penetrationNumber < 1)
                lifeTime = 0;
        }
    }
}
