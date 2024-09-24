using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform playerTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float hitWaitTime;        //攻击间隔
    private float hitCounter;                                 //
    [SerializeField] private float health;
    private float curHealth;
    [SerializeField] private float knockBackTime;   
    private float knockBackCounter;
    [SerializeField] private int expToGive;             //经验值
    [SerializeField] private int coinValue;             //金币值
    [SerializeField] private float coinDropRate;   //金币掉率
    private bool isRespawn=true;
    protected virtual void Start()
    {
        rig = GetComponent<Rigidbody2D>();    
        playerTransform=PlayerHealthController.instance.transform;
        curHealth = health;
    }

    protected virtual void Update()
    {
        if (isRespawn) 
        {
            curHealth = health;
            isRespawn=false;
        }

        if (hitCounter > 0.0f)
            hitCounter -= Time.deltaTime;

        KonckBack();

    }
    protected virtual void FixedUpdate()
    {
        if (PlayerController.instance.gameObject.activeSelf)
            rig.velocity = (playerTransform.position - transform.position).normalized * moveSpeed;
        else
            rig.velocity = (transform.position-playerTransform.position).normalized*moveSpeed*1.2f;
    }
    protected void KonckBack()
    {
        if (knockBackCounter > 0.0f)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0.0f)
                moveSpeed = -moveSpeed * 2.0f;
            if (knockBackCounter <= 0.0f)
                moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player"&&hitCounter<=0.0f) 
        {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake) 
    {
        curHealth-=damageToTake;
        if (curHealth <= 0.0f) 
        {
            ExperenceLevelController.instance.SpwanExp(transform.position,expToGive);
            if (Random.value <= coinDropRate)
                CoinController.instance.DropCoin(transform.position,coinValue);
            AudioController.instance.PlaySFXPiched(6);
            isRespawn = true;
            ObjectPool.Instance.PushObject(gameObject);
        }
        AudioController.instance.PlaySFXPiched(5);
        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }
    public void TakeDamage(float damageToTake,bool shouldKonckBack)
    {
        TakeDamage(damageToTake);
        if (shouldKonckBack)
            knockBackCounter=knockBackTime;
    }


}
