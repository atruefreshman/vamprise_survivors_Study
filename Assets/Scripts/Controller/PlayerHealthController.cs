using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;
    private void Awake()
    {
        instance = this;
    }

    public float maxHealth;
    [HideInInspector]public float curHealth;
     private Slider healthSliderL;
     private Slider healthSliderR;
    public GameObject deathEffect;
    void Start()
    {
        curHealth = maxHealth;
        healthSliderL=transform.Find("HealthCanvas/Sliderl").GetComponent<Slider>();
        healthSliderR = transform.Find("HealthCanvas/SliderR").GetComponent<Slider>();
        healthSliderL.maxValue = maxHealth;
        healthSliderL.value = curHealth;
        healthSliderR.maxValue = curHealth;
        healthSliderR.value = curHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damageTotake) 
    {
        if (curHealth - damageTotake <= 0)
        {
            curHealth = 0;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            LevelManager.Instance.EndLevel();
        }
        else 
        {
            AudioController.instance.PlaySFXPiched(4);
            curHealth -= damageTotake;
        }
        healthSliderL.value = curHealth;
        healthSliderR.value = curHealth;

    }
}
