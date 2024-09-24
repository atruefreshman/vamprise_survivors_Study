using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float aliveTime;
    private float curAliveTime;
    private Vector3 direction;
    void Start()
    {
        curAliveTime = aliveTime;
        direction = (PlayerHealthController.instance.transform.position - transform.position).normalized;
    }

    private void OnEnable()
    {
        curAliveTime = aliveTime;
        direction = (PlayerHealthController.instance.transform.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position=transform.position+direction*speed*Time.deltaTime;
        curAliveTime-= Time.deltaTime;
        if(curAliveTime < 0)
            ObjectPool.Instance.PushObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player") 
        {
            PlayerHealthController.instance.TakeDamage(damage);
            ObjectPool.Instance.PushObject(gameObject);
            curAliveTime = aliveTime;
        }
        if (other.gameObject.tag == "Sword") 
        {
            ObjectPool.Instance.PushObject(gameObject);
            curAliveTime= aliveTime;
        }
    }
}
