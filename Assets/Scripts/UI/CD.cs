using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CD : MonoBehaviour
{
    public SwordWeapon weapon;
    Image image;
    private Color color;
    void Start()
    {
        image = GetComponent<Image>();
        color=image.color;
    }

    void Update()
    {
        image.fillAmount = (weapon.timeBetweenSpawn - weapon.spawnCounter)/weapon.timeBetweenSpawn;
        if(image.fillAmount>0.99f)
            image.color = Color.red;
        else image.color = color;
    }
}
