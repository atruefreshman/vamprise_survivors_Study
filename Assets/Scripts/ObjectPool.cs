using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private void Awake()
    {
        Instance = this;    
    }
    /* public static ObjectPool Instance 
     {
         get 
         {
             if (instance == null)
                 instance = new ObjectPool();
             return instance;
         }
     } */

    [SerializeField]private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    



    public GameObject GetObject(GameObject go) 
    {
        GameObject gameObject;
        if (!objectPool.ContainsKey(go.name) || objectPool[go.name].Count == 0) 
        {
            gameObject=GameObject.Instantiate(go);
            PushObject(gameObject);
            Transform childTransform =transform.Find(go.name);
            if (!childTransform)                                                                   //一级子物体
            {
                GameObject childG = new GameObject(go.name);
                childG.transform.SetParent(transform);
                childTransform=childG.transform;
            }
            gameObject.transform.SetParent(childTransform);    //级子物体（物体本身）
        }
        gameObject = objectPool[go.name].Dequeue();
        gameObject.SetActive(true);
        return gameObject;
    }

    public void PushObject(GameObject go) 
    {
        string name=go.name.Replace("(Clone)",string.Empty);
        if(!objectPool.ContainsKey(name))
            objectPool.Add(name, new Queue<GameObject>());
        objectPool[name].Enqueue(go);
        go.SetActive(false);
    }

}
