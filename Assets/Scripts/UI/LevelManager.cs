using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private bool gameActive;
    public float timer;
    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if (gameActive) 
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    public void EndLevel() 
    {
        gameActive = false;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        UIController.instance.aliveTimeText.text=minutes.ToString()+" m "+seconds.ToString("00"+" s");
        StartCoroutine(WaitForTimes());
    }
    IEnumerator WaitForTimes() 
    {
        yield return new WaitForSeconds(0.8f);
        UIController.instance.endPanel.SetActive(true);
    }
}
