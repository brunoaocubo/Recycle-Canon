using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyCategory
{
    Organic, 
    Plastic, 
    Metal
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject organicEnemyPrefab;
    [SerializeField] private GameObject plasticEnemyPrefab;
    [SerializeField] private GameObject metalEnemyPrefab;
    [SerializeField] private float spawnDelay = 2.0f;
    [SerializeField] private float spawnAreaWidth = 5.0f;
    [SerializeField] private float spawnAreaHeight = 2.0f;
    [SerializeField] private int numWaves = 5;
    [SerializeField] private int numEnemiesPerWave = 10;
    [SerializeField] private float waveDuration = 20.0f;
    
    private EnemyCategory typeEnemy;
    public EnemyCategory TypeEnemy { get => typeEnemy; }

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
                float randomValue = Random.Range(0f,3f);
                if (randomValue < 1.3f)
                {
                    enemyPrefab = organicEnemyPrefab;
                    typeEnemy = EnemyCategory.Organic;
                }
                else if (randomValue < 2f)
                {
                    enemyPrefab = plasticEnemyPrefab;
                    typeEnemy = EnemyCategory.Plastic;
                }
                else
                {
                    enemyPrefab = metalEnemyPrefab;
                    typeEnemy = EnemyCategory.Metal;
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