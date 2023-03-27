using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text trashOrganicText;
    public Text trashPlasticText;
    public Text trashMetalText;
    public Text organicAmmoText;
    public Text plasticAmmoText;
    public Text metalAmmoText;

    [SerializeField] private CollectorContainer collectorContainer;
    private Player player;


    public void SetCollectorContainer(CollectorContainer collectorContainer)
    {
        this.collectorContainer = collectorContainer;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    void Update()
    {
        // Atualiza as informa��es de texto com as quantidades atuais
        //trashOrganicText.text = "Org�nico: " + collectorContainer.AmmountTrashOrganic.ToString();
        //trashPlasticText.text = "Pl�stico: " + collectorContainer.AmmountTrashPlastic.ToString();
        //trashMetalText.text = "Metal: " + collectorContainer.AmmountTrashMetal.ToString();
        organicAmmoText.text = collectorContainer.OrganicAmmo.ToString();
        plasticAmmoText.text = collectorContainer.PlasticAmmo.ToString();
        metalAmmoText.text = collectorContainer.MetalAmmo.ToString();

    }
}
