using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{

    [HideInInspector] [SerializeField] protected float damageAmount;
    public float DamageAmount { set { damageAmount=value; } }
    [HideInInspector][SerializeField] protected float lifeTime;
    public float LifeTime { set {  lifeTime=value; } }
    [SerializeField]private float growSpeed;
    private Vector3 targetSize;
    [SerializeField] protected bool shouldKonckBack;

    public bool useExtraLifeTime;
    private float extraLifeTime = 1000000;
    protected virtual void Start()
    {
        if (useExtraLifeTime)
            lifeTime = extraLifeTime;
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }


    protected virtual void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0.0f) 
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0.0f) 
            {
                if (transform.childCount>0)
                    if (transform.parent!=null && transform.parent.name=="Holder(Clone)")
                        Destroy(transform.parent.gameObject);
                Destroy(gameObject);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            other.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKonckBack);
        }
    }


}
