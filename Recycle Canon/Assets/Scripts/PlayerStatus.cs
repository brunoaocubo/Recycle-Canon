using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int lives = 3;

    public int Lives { get => lives; }

    public void TakeDamage(int valueDamage) 
    {
        lives -= valueDamage;

        if(lives <= 0) 
        {
            lives = 0;
            Debug.Log("Vida do player chegou a 0");
        }
    }
    public void IncreaseLifePlayer(int valueHeal)
    {
        lives += valueHeal;

        if(lives >= 3) 
        {
            lives = 3;
            Debug.Log("Vida do player chegou a 3");

        }
    }
}
