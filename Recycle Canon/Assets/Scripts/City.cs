using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    private float health = 20;
    public float Health { get => health; }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;

        if (health <= 0)
        {
            health = 0;
            Debug.Log("Vida da cidade chegou a 0");
        }
    }

    public void IncreaseHealth(float healthValue) 
    {
        health += healthValue;
    }
}
