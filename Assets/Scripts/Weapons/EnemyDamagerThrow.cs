using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagerThrow : EnemyDamager
{
    private Rigidbody2D rg;
    public float throwPower;
    protected override void Start()
    { 
        base.Start();
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower * 1.43f);
    }

    protected override void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z+450*Time.deltaTime*Mathf.Sign(rg.velocity.x)); ;
        base.Update();
    }
}
