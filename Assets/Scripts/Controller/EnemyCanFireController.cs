using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanFireController : EnemyController
{
    [SerializeField] private float fireTimeSpan;
    private GameObject bullet;
    private float curFireTimeSpan;

    protected override void Start()
    {
        base.Start();
        bullet = transform.Find("Bullet").gameObject;
        curFireTimeSpan = fireTimeSpan;
    }

    private void OnEnable()
    {
        curFireTimeSpan = fireTimeSpan;
    }

    protected override void Update()
    {
        curFireTimeSpan -= Time.deltaTime;
        if (curFireTimeSpan < 0) 
        {
            if (Vector3.Distance(PlayerHealthController.instance.transform.position, transform.position) < 8f) 
            {
                curFireTimeSpan += fireTimeSpan;
                ObjectPool.Instance.GetObject(bullet).transform.position = transform.position;
            }          
        }
        base.Update();
    }

    
}
