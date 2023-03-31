using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private int lives = 3;

    public int Lives { get => lives; }

    public void TakeDamage(int valueDamage)
    {
        lives -= valueDamage;

        if (lives <= 0)
        {
            lives = 0;
            Destroy(gameObject);
        }
    }

    public void TakeDamageBoss(int valueDamage) 
    {
        lives -= valueDamage;

        if (lives <= 0)
        {
            lives = 0;
            Destroy(gameObject);
        }
    }
}
