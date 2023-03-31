using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    private int livesEnemy = 3;
    public int LivesEnemy { get => livesEnemy; }

    private int livesBoss = 20;
    public int LivesBoss { get => livesEnemy; }

    private bool isDead = false;
    public bool IsDead { get => isDead; }

    public void TakeDamage(int valueDamage)
    {
        livesEnemy -= valueDamage;

        if (livesEnemy <= 0)
        {
            livesEnemy = 0;
            Destroy(gameObject);
        }
    }

    public void TakeDamageBoss(int valueDamage) 
    {
        livesBoss -= valueDamage;

        if (livesBoss <= 0)
        {
            livesBoss = 0;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(livesBoss <= 0) 
        {
            isDead = true;
        }
    }
}
