using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;
    [SerializeField] private float lifeTime;
    private float lifeCounter;
    [SerializeField] private float floatSpeed;

    void Update()
    {
        if (lifeCounter > 0) 
        {
            lifeCounter -= Time.deltaTime;
            if (lifeCounter <= 0.01f)
                DamageNumberController.instance.PlaceInPool(this);
        }

        transform.position+=Vector3.up*floatSpeed*Time.deltaTime;
    }

    public void Setup(int damageDisplay) 
    {
        lifeCounter = lifeTime;
        damageText.text=damageDisplay.ToString();
    }

}
