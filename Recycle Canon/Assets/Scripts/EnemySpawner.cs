using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject organicEnemyPrefab;
    public GameObject plasticEnemyPrefab;
    public GameObject metalEnemyPrefab;

    public float spawnDelay = 2.0f;
    public float spawnAreaWidth = 5.0f;
    public float spawnAreaHeight = 2.0f;
    public int numWaves = 5;
    public int numEnemiesPerWave = 10;
    public float waveDuration = 20.0f;

    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    IEnumerator SpawnEnemies()
    {
        while (currentWave < numWaves)
        {
            for (int i = 0; i < numEnemiesPerWave; i++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2), 0, Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2));
                
                GameObject enemyPrefab;
                float randomValue = Random.value;
                if (randomValue < 0.7f)
                {
                    enemyPrefab = organicEnemyPrefab;
                }
                else if (randomValue < 0.9f)
                {
                    enemyPrefab = plasticEnemyPrefab;
                }
                else
                {
                    enemyPrefab = metalEnemyPrefab;
                }

                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemiesSpawned++;

                yield return new WaitForSeconds(spawnDelay);
            }

            currentWave++;

            if (currentWave < numWaves)
            {
                yield return new WaitForSeconds(waveDuration);
            }
        }
    }
}