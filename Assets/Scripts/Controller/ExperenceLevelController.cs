using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperenceLevelController : MonoBehaviour
{
    public static ExperenceLevelController instance;
    void Awake()
    {
        instance = this; 
    }

    private int curExperence;
    public ExpPickup expPrefeb;

    [SerializeField] private List<int> expLevels;
    private int cueLevel;
    private int levelCount = 100;

    private List<Weapon> weaponsToUpgrade;  

    void Start()
    {
        weaponsToUpgrade=new List<Weapon>();
        while (expLevels.Count< levelCount)      //设置每一级的经验需求
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count-1]+1));
    }

    public void GetExp(int amountToGet) 
    {
        curExperence += amountToGet;

        if (curExperence >= expLevels[cueLevel])
            LevelUP();
        UIController.instance.UpdateExperence(curExperence, expLevels[cueLevel],cueLevel);
    }

    public void SpwanExp(Vector3 position,int expVal) 
    {
        ExpPickup exp = ObjectPool.Instance.GetObject(expPrefeb.gameObject).GetComponent<ExpPickup>();
        exp.ExpVal = expVal;
        exp.transform.position = position;
        exp.transform.rotation = Quaternion.identity;
    }

    public void LevelUP() 
    {
        curExperence -= expLevels[cueLevel];
        cueLevel++;
        if(cueLevel>=expLevels.Count)
            cueLevel=expLevels.Count-1;
        UIController.instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0;
        weaponsToUpgrade.Clear();
        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assigneWeapons);
        if (availableWeapons.Count > 0) 
        {
            int selected=Random.Range(0,availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }
        if(PlayerController.instance.assigneWeapons.Count+PlayerController.instance.fullyLevelledWeapons.Count<PlayerController.instance.maxWeapons)
            availableWeapons.AddRange(PlayerController.instance.unassigneWeapons);
        for (int i = weaponsToUpgrade.Count; i < UIController.instance.levelUpSelectionButtons.Length; i++) 
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }
        for (int i = 0; i < weaponsToUpgrade.Count; i++) 
            UIController.instance.levelUpSelectionButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        for(int i=0;i<UIController.instance.levelUpSelectionButtons.Length;i++) 
        {
            if(i<weaponsToUpgrade.Count)
                UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(true);
            else
                UIController.instance.levelUpSelectionButtons[i].gameObject.SetActive(false);
        }
    }

}
