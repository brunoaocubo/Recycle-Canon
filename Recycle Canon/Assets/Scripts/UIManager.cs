using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
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


    private void Start()
    {
        healthCity.maxValue = cityStatus.Health;
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
        trashOrganicText.text = collectorContainer.AmmountTrashOrganic.ToString();
        trashPlasticText.text = collectorContainer.AmmountTrashPlastic.ToString();
        trashMetalText.text = collectorContainer.AmmountTrashMetal.ToString();
        organicAmmoText.text = collectorContainer.OrganicAmmo.ToString();
        plasticAmmoText.text = collectorContainer.PlasticAmmo.ToString();
        metalAmmoText.text = collectorContainer.MetalAmmo.ToString();
        livesPlayerText.text = playerStatus.Lives.ToString();
        healthCity.value = cityStatus.Health;
    }
}
