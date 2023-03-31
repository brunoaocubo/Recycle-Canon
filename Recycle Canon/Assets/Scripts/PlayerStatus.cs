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
        }
    }
    public void IncreaseHealth(int valueHeal)
    {
        lives += valueHeal;

        if(lives >= 3) 
        {
            lives = 3;
        }
    }
}
