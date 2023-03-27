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

    [SerializeField] private CollectorContainer collectorContainer;
    private Player player;

    private void Start()
    {

    }

    private void SetCollectorContainer(CollectorContainer collectorContainer)
    {
        this.collectorContainer = collectorContainer;
    }

    private void SetPlayer(Player player)
    {
        this.player = player;
    }

    void Update()
    {
        trashOrganicText.text = collectorContainer.AmmountTrashOrganic.ToString();
        trashPlasticText.text = collectorContainer.AmmountTrashPlastic.ToString();
        trashMetalText.text = collectorContainer.AmmountTrashMetal.ToString();
        organicAmmoText.text = collectorContainer.OrganicAmmo.ToString();
        plasticAmmoText.text = collectorContainer.PlasticAmmo.ToString();
        metalAmmoText.text = collectorContainer.MetalAmmo.ToString();
    }
}
