using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : Weapon
{

    [SerializeField] private float ratateSpeed;
    private Transform rotation,fireballToSpan;
    private float timeBetweenSpawn;
    private float spawnCounter;
    public EnemyDamager enemyDamager;
    void Start()
    {
        rotation = transform.Find("Rotation").GetComponent<Transform>();
        fireballToSpan= transform.Find("Holder").GetComponent<Transform>();
        SetStats();
    }


    void Update()
    {
        rotation.rotation = Quaternion.Euler(0.0f, 0.0f, rotation.rotation.eulerAngles.z + (ratateSpeed * Time.deltaTime * weaponStates[weaponLevel].Speed));
        spawnCounter-=Time.deltaTime;
        if (spawnCounter <= 0) 
        {
            spawnCounter = timeBetweenSpawn;
            for (int i = 0; i < weaponStates[weaponLevel].Amount;i++) 
            {
                float rotation = 360f / weaponStates[weaponLevel].Amount * i;
                Instantiate(fireballToSpan, fireballToSpan.position, Quaternion.Euler(0f,0f, rotation), this.rotation).gameObject.SetActive(true);
            }
        }

        if (statsUpdated) 
        {
            statsUpdated=false;
            SetStats();
        }
    }

    public void SetStats() 
    {
        enemyDamager.DamageAmount = weaponStates[weaponLevel].Damage;   //�˺�
        transform.localScale = Vector3.one*weaponStates[weaponLevel].Range;      //��С
        timeBetweenSpawn = weaponStates[weaponLevel].TimeBetweenSpawn;    //���ɼ��
        enemyDamager.LifeTime = weaponStates[weaponLevel].Duration;              //����ʱ��
    }

}
