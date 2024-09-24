using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{

    [SerializeField] private int expVal;
    public int ExpVal { set { expVal = value;  } }
    private bool movingToPlayer;
    [SerializeField] private float moveSpeed;
    private float stopWaitTime = 0.3f;

    void Update()
    {
        if (movingToPlayer) 
        {
            stopWaitTime-=Time.deltaTime;
            if (stopWaitTime < 0)
                transform.position = Vector3.MoveTowards(transform.position, PlayerHealthController.instance.transform.position, moveSpeed * Time.deltaTime);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            movingToPlayer = false;
            ExperenceLevelController.instance.GetExp(expVal);
            AudioController.instance.PlaySFXPiched(0);
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.tag == "PickupRange") 
        {
            movingToPlayer = true;
        }
    }

}
