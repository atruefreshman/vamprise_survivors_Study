using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance=this;
        Time.timeScale=1.0f;
    }

    private Slider explelSlider;
    private TMP_Text expleText;
    private TMP_Text coinText;
    private TMP_Text timeText;
    [HideInInspector]public TMP_Text aliveTimeText;

    public LevelUpSelectionButton[] levelUpSelectionButtons;
    [HideInInspector]public GameObject levelUpPanel;
    [HideInInspector] public GameObject shopPanel;
    [HideInInspector] public GameObject endPanel;
    [HideInInspector] public GameObject pausePanel;

    [HideInInspector] public PlayerStateUpgradeDisplay moveSpeed;
    [HideInInspector] public PlayerStateUpgradeDisplay health;
    [HideInInspector] public PlayerStateUpgradeDisplay pickupRange;
    [HideInInspector] public PlayerStateUpgradeDisplay maxWeapon;
    void Start()
    {
        explelSlider = GetComponentInChildren<Slider>();
        expleText = transform.Find("LevelText").GetComponent<TMP_Text>();
        coinText = transform.Find("CoinText").GetComponent<TMP_Text>();
        timeText = transform.Find("TimeText").GetComponent<TMP_Text>();
        aliveTimeText = transform.Find("EndPanel/AliveTimeText").GetComponent <TMP_Text>(); 
        levelUpPanel = transform.Find("LevelUpInterface").gameObject;
        shopPanel= transform.Find("ShopPanel").gameObject;
        endPanel= transform.Find("EndPanel").gameObject;
        pausePanel = transform.Find("PausePanel").gameObject;

        moveSpeed = transform.Find("ShopPanel/MoveSpeedUpgrade").GetComponent<PlayerStateUpgradeDisplay>();
        health = transform.Find("ShopPanel/HealthUpgrade").GetComponent<PlayerStateUpgradeDisplay>();
        pickupRange = transform.Find("ShopPanel/PickupRangeUpgrade").GetComponent<PlayerStateUpgradeDisplay>();
        maxWeapon = transform.Find("ShopPanel/MaxWeaponUpgrade").GetComponent<PlayerStateUpgradeDisplay>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }


    public void UpdateExperence(int curExp,int levelExp,int curLevel) 
    {
        explelSlider.maxValue = levelExp;
        explelSlider.value = curExp;
        expleText.text = "Level : " + curLevel;
    }

    public void ClosePanel() 
    {
        levelUpPanel.SetActive(false);
        Time.timeScale= 1.0f;   
    }

    public void OpenShop() 
    {
        levelUpPanel.SetActive(false);
        shopPanel.SetActive(true);
        PlayerStateController.instance.UpdateDisplay();
    }

    public void OpenLevelUpPanel() 
    {
        shopPanel.SetActive(false);
        levelUpPanel.SetActive(true);
    }

    public void UpdateCoinText()
    {
        coinText.text = CoinController.instance.CoinAmount.ToString();
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStateController.instance.PurchaseMoveSpeed();
    }
    public void Purchasehealth()
    {
        PlayerStateController.instance.Purchasehealth();
    }
    public void PurchasepickupRange()
    {
        PlayerStateController.instance.PurchasepickupRange();
    }
    public void PurchaseMaxWeapons()
    {
        PlayerStateController.instance.PurchaseMaxWeapons();
    }

    public void UpdateTimer(float time) 
    {
        float minutes=Mathf.FloorToInt(time / 60);
        float seconds= Mathf.FloorToInt(time % 60);
        timeText.text = "Alive Time   "+minutes+" : "+seconds.ToString("00");
    }

    public void OpenMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame() 
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
    public void PauseGame() 
    {
        if (levelUpPanel.activeSelf == true || shopPanel.activeSelf == true || endPanel.activeSelf == true)
            return;
        if (pausePanel.activeSelf == false) 
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
            ResumeGame();
    }
    public void ResumeGame() 
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
