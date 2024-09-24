using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;
    private void Awake()
    {
        instance = this;
    }

    [HideInInspector] [SerializeField]private int curCoins;
    public int CoinAmount { get { return curCoins; } }
    [SerializeField] private CoinPickup coin;
    
    public void AddCoins(int coinAmount) 
    {
        curCoins += coinAmount;
        UIController.instance.UpdateCoinText();
    }

    public void DropCoin(Vector3 position,int value) 
    {

        GameObject gameObject = ObjectPool.Instance.GetObject(coin.gameObject);
        CoinPickup newCoin=gameObject.GetComponent<CoinPickup>();
        newCoin.transform.position = position + new Vector3(0, .45f, 0);
        newCoin.transform.rotation = Quaternion.identity;
        newCoin.CoinValue = value;  
        newCoin.gameObject.SetActive(true); 
    }

}
