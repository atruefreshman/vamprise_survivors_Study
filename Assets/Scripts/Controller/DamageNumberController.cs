using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;
    void Awake()
    {
        instance = this;
    }

    private Transform numberCanvas;
    private DamageNumber numberToSpawn;

    [SerializeField]private List<DamageNumber> numberPool=new List<DamageNumber>();

    void Start()
    {
        numberCanvas=transform.Find("DamageNumberCanvas").GetComponent<Transform>();
        numberToSpawn=transform.Find("DamageNumberCanvas/Text (TMP)").GetComponent<DamageNumber>();
    }

    public void SpawnDamage(float damageAmount,Vector3 location) 
    {
        int rounded=Mathf.RoundToInt(damageAmount);
        //DamageNumber newDamage = Instantiate(numberToSpawn,location,Quaternion.identity,numberCanvas);
        DamageNumber newDamage = GetFromPool();
        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;    
        newDamage.Setup(rounded);
    }

    public DamageNumber GetFromPool() 
    {
        DamageNumber numberToOutput = null;
        if (numberPool.Count == 0)
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        else 
        {
            numberToOutput = numberPool[0];
            numberPool.RemoveAt(0);
        }
        return numberToOutput;
    }
    public void PlaceInPool(DamageNumber numberToPlace) 
    {
        numberToPlace.gameObject.SetActive(false);
        numberPool.Add(numberToPlace);
    }

}
