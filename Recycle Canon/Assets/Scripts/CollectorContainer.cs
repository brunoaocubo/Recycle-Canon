using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorContainer : MonoBehaviour
{
    private int ammountTrashOrganic;
    private int ammountTrashPlastic;
    private int ammountTrashMetal;   
    private int organicAmmo;
    private int plasticAmmo;
    private int metalAmmo;
    
    public int AmmountTrashOrganic { get => ammountTrashOrganic; }                  //set => ammountTrashOrganic = value; 
    public int AmmountTrashPlastic { get => ammountTrashPlastic; }
    public int AmmountTrashMetal { get => ammountTrashMetal; }
    public int OrganicAmmo { get => organicAmmo; }                                          
    public int PlasticAmmo { get => plasticAmmo; }                                          
    public int MetalAmmo { get => metalAmmo; }

    public void AddTrashOrganic(int TrashOrganic) 
    {
        ammountTrashOrganic += TrashOrganic;
    }

    public void ReloadOrganicAmmo()
    {
        organicAmmo += ammountTrashOrganic;
        ammountTrashOrganic = 0;
    }

    public void DecreaseOrganicAmmo() 
    {
        organicAmmo--;
    }

    public void AddTrashPlastic(int TrashPlastic)
    {
        ammountTrashPlastic += TrashPlastic;
    }

    public void ReloadPlasticAmmo()
    {
        plasticAmmo += ammountTrashPlastic;
        ammountTrashPlastic = 0;
    }

    public void DecreasePlasticAmmo()
    {
        plasticAmmo--;
    }

    public void AddTrashMetal(int TrashMetal)
    {
        ammountTrashMetal += TrashMetal;
    }

    public void ReloadMetalAmmo()
    {
        metalAmmo += ammountTrashMetal;
        ammountTrashMetal = 0;
    }

    public void DecreaseMetalAmmo()
    {
        metalAmmo--;
    }
}
