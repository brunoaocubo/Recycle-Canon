using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panelSettingsScene;
    [SerializeField] private GameObject buttonSettingsScene;
    [SerializeField] private GameObject buttonResumeGame;
    [SerializeField] private GameObject buttonReloadGame;
    [SerializeField] private GameObject[] lifePlayer = new GameObject[3];

    [SerializeField] private GameObject levelCompletedText;
    [SerializeField] private GameObject levelFailedText;
    [SerializeField] private TMP_Text trashOrganicText;
    [SerializeField] private TMP_Text trashPlasticText;
    [SerializeField] private TMP_Text trashMetalText;
    [SerializeField] private Text organicAmmoText;
    [SerializeField] private Text plasticAmmoText;
    [SerializeField] private Text metalAmmoText;
    [SerializeField] private Slider healthCity;

    [SerializeField] private CollectorContainer collectorContainer;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private EnemyStatus enemyStatus;
    [SerializeField] private City cityStatus;

    private void Start()
    {
        healthCity.maxValue = cityStatus.Health;
        enemyStatus = FindObjectOfType<EnemyStatus>();
        panelSettingsScene.SetActive(false);
        
    }

    private void SetCollectorContainer(CollectorContainer collectorContainer)
    {
        this.collectorContainer = collectorContainer;
    }

    private void SetPlayerStatus(PlayerStatus playerStatus)
    {
        this.playerStatus = playerStatus;
    }

    private void SetCityStatus(City cityStatus)
    {
        this.cityStatus = cityStatus;
    }

    void Update()
    {
        UIGeneralUpdate();
        UIStatusPlayer();
        UIStatusGame();

        if (levelCompletedText.activeSelf)
        {
            levelFailedText.SetActive(false);
            levelCompletedText.SetActive(true);
        }
        else
        {
            levelFailedText.SetActive(true);
            levelCompletedText.SetActive(false);
        }
    }

    private void UIStatusGame() 
    {
        if (playerStatus.Lives > 0)
        {
           
            if (enemyStatus.IsDead)
            {
                panelSettingsScene.SetActive(true);
                buttonResumeGame.SetActive(true);
                buttonReloadGame.SetActive(false);
                levelCompletedText.SetActive(true);
                FreezeGame(true);
            }
        }
        else 
        {
            levelCompletedText.SetActive(false);
        }
    }

    private void UIStatusPlayer() 
    {
        if (playerStatus.Lives <= 0 || cityStatus.Health <= 0)
        {
            panelSettingsScene.SetActive(true);
            buttonResumeGame.SetActive(false);
            buttonReloadGame.SetActive(true);
            levelCompletedText.SetActive(false);
            FreezeGame(true);
        }
        else
        {
            FreezeGame(false);
        }

        for (int i = 0; i < lifePlayer.Length; i++)
        {
            if (i < playerStatus.Lives)
            {
                lifePlayer[i].SetActive(true);
            }
            else
            {
                lifePlayer[i].SetActive(false);
            }
        }
    }

    private void UIGeneralUpdate () 
    {
        trashOrganicText.text = collectorContainer.AmmountTrashOrganic.ToString();
        trashPlasticText.text = collectorContainer.AmmountTrashPlastic.ToString();
        trashMetalText.text = collectorContainer.AmmountTrashMetal.ToString();
        organicAmmoText.text = collectorContainer.OrganicAmmo.ToString();
        plasticAmmoText.text = collectorContainer.PlasticAmmo.ToString();
        metalAmmoText.text = collectorContainer.MetalAmmo.ToString();
        healthCity.value = cityStatus.Health;
    }

    public void OpenSettingsScene()
    {
        FreezeGame(true);
        panelSettingsScene.SetActive(true);
        buttonSettingsScene.SetActive(false);
        buttonResumeGame.SetActive(true);
        buttonReloadGame.SetActive(false);  
    }

    public void ResumeScene()
    {
        FreezeGame(false);
        panelSettingsScene.SetActive(false);
        buttonSettingsScene.SetActive(true);
    }

    public void LoadHome()
    {
        FreezeGame(false);
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void FreezeGame(bool freeze) 
    {
        if (freeze) {Time.timeScale = 0;}
        else {Time.timeScale = 1;} 
    }
}