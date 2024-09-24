using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    private float spawnCounter;
    private Transform minSpawn;
    private Transform maxSpawn;
    private Transform target;
    private float deSpawnDiatance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    [SerializeField] private int checkPerFrame;
     private int enemyToCheck;

    [SerializeField] private List<WaveInfo> waveInfos;   //波次敌人信息
    private int currentWave;         //第几波
    private float waveCounter;    //波次持续时间

    private bool hasSpawnBoss;

    void Start()
    {
        spawnCounter = timeToSpawn;
        minSpawn=transform.Find("MinSpawnPoint").GetComponent<Transform>();
        maxSpawn=transform.Find("MaxSpawnPoint").GetComponent<Transform>();
        target=PlayerHealthController.instance.transform;
        deSpawnDiatance = Vector3.Distance(transform.position,maxSpawn.position)+8.0f;
        currentWave = -1;
        GoToNextWave();
    }

    void Update()
    {

        if (!PlayerHealthController.instance.gameObject.activeSelf)
            return;
        //控制波次和敌人生成
        if (currentWave < waveInfos.Count) 
        {
            waveCounter-=Time.deltaTime;
            if (waveCounter <= 0) 
                GoToNextWave();
            spawnCounter-=Time.deltaTime;
            if(spawnCounter <= 0) 
            {
                spawnCounter = waveInfos[currentWave].TimeBetweenSpawns;
                int enemyKinds = waveInfos[currentWave].EnemyToSpawn.Length;
                GameObject newEnemy = null;
                if (!hasSpawnBoss&&enemyKinds >=4) 
                {
                    for (int i = 3; i <enemyKinds; i++) 
                    {
                        newEnemy = ObjectPool.Instance.GetObject(waveInfos[currentWave].EnemyToSpawn[i]);
                        newEnemy.transform.position = SelectSpawnPoint();
                        newEnemy.transform.rotation = Quaternion.identity;
                        spawnedEnemies.Add(newEnemy);
                    }
                    hasSpawnBoss = true;
                }
                string[] proportion =  waveInfos[currentWave].Proportion.Split(':');
                int check = UnityEngine.Random.Range(1, 11);                
                for (int i = 0; i < enemyKinds; i++) 
                {                 
                    if (check <= float.Parse(proportion[i])) 
                    {
                        newEnemy = ObjectPool.Instance.GetObject(waveInfos[currentWave].EnemyToSpawn[i]);
                        newEnemy.transform.position = SelectSpawnPoint();
                        newEnemy.transform.rotation = Quaternion.identity;
                        spawnedEnemies.Add(newEnemy);
                        break;
                    }
                }                    
            }
        }

        transform.position = target.position;
        int checkTarget = enemyToCheck + checkPerFrame;
        while (enemyToCheck < checkTarget) 
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > deSpawnDiatance)
                    {
                        ObjectPool.Instance.PushObject(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                        enemyToCheck++;
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else 
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPoint() 
    {
        Vector3 spawnPoint = Vector3.zero;

        if (UnityEngine.Random.Range(0.0f, 1f) > 0.5625f)
        {
            spawnPoint.y = UnityEngine.Random.Range(minSpawn.position.y, maxSpawn.position.y);
            if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f)
                spawnPoint.x = minSpawn.position.x;
            else
                spawnPoint.x = maxSpawn.position.x;
        }
        else 
        {
            spawnPoint.x = UnityEngine.Random.Range(minSpawn.position.x,maxSpawn.position.x);
            if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f)
                spawnPoint.y = minSpawn.position.y;
            else
                spawnPoint.y = maxSpawn.position.y;
        }
        return spawnPoint;
    }

    public void GoToNextWave() 
    {
        currentWave++;
        if(currentWave>=waveInfos.Count)
            currentWave = waveInfos.Count-1;
        waveCounter = waveInfos[currentWave].WaveLength;
        spawnCounter = waveInfos[currentWave].TimeBetweenSpawns;
        hasSpawnBoss = false;
    }

}

[Serializable]
public class WaveInfo
{
    [SerializeField] GameObject[] enemyToSpawn;          //要生成的敌人
    public GameObject[] EnemyToSpawn { get { return enemyToSpawn; } }
    [SerializeField] private string proportion;
    public string Proportion { get { return proportion; } }
    [SerializeField] private float waveLength;                           //波次时长
    public float WaveLength { get { return waveLength; } }
    [SerializeField] private float timeBetweenSpawns;             //怪物生成间隔
    public float TimeBetweenSpawns { get { return timeBetweenSpawns; } }
}
