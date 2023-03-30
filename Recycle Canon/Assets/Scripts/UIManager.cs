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


    [SerializeField] private TMP_Text trashOrganicText;
    [SerializeField] private TMP_Text trashPlasticText;
    [SerializeField] private TMP_Text trashMetalText;
    [SerializeField] private Text organicAmmoText;
    [SerializeField] private Text plasticAmmoText;
    [SerializeField] private Text metalAmmoText;
    [SerializeField] private TMP_Text livesPlayerText;
    [SerializeField] private Slider healthCity;

    [SerializeField] private CollectorContainer collectorContainer;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private City cityStatus;

    private bool completed;
    private bool phaseFail;


    private void Start()
    {
        healthCity.maxValue = cityStatus.Health;
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
        if(playerStatus.Lives <= 0 || cityStatus.Health <= 0) 
        {
            phaseFail = true;
            panelSettingsScene.SetActive(true);

            FreezeGame(true);
        }

        trashOrganicText.text = collectorContainer.AmmountTrashOrganic.ToString();
        trashPlasticText.text = collectorContainer.AmmountTrashPlastic.ToString();
        trashMetalText.text = collectorContainer.AmmountTrashMetal.ToString();
        organicAmmoText.text = collectorContainer.OrganicAmmo.ToString();
        plasticAmmoText.text = collectorContainer.PlasticAmmo.ToString();
        metalAmmoText.text = collectorContainer.MetalAmmo.ToString();
        livesPlayerText.text = playerStatus.Lives.ToString();
        healthCity.value = cityStatus.Health;
    }

    public void OpenSettingsScene()
    {
        panelSettingsScene.SetActive(true);
        buttonSettingsScene.SetActive(false);
        buttonResumeGame.SetActive(true);
        buttonReloadGame.SetActive(false);
        FreezeGame(true);    
    }

    public void ResumeScene()
    {
        panelSettingsScene.SetActive(false);
        buttonSettingsScene.SetActive(true);
        FreezeGame(false);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FreezeGame(bool freeze) 
    {
        if (freeze) {Time.timeScale = 0;}
        else {Time.timeScale = 1;} 
    }
}
