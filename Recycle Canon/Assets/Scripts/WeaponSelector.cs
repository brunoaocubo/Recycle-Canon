using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private Canon canon;

    private void Start()
    {
        canon = GetComponent<Canon>();
    }

    public void SelectOrganicAmmo() 
    {
        canon.SelectAmmoType(AmmoType.Organic);
    }

    public void SelectPlasticAmmo()
    {
        canon.SelectAmmoType(AmmoType.Plastic);

    }

    public void SelectMetalAmmo()
    {
        canon.SelectAmmoType(AmmoType.Metal);
    }
}
