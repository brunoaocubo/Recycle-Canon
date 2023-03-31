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
    private EnemyCategory typeEnemy;

    [SerializeField] private City cityStatus;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject organicEnemyPrefab;
    [SerializeField] private GameObject plasticEnemyPrefab;
    [SerializeField] private GameObject metalEnemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPositionBoss;
    [SerializeField] private float spawnDelay = 2.0f;
    [SerializeField] private float spawnAreaWidth = 5.0f;
    [SerializeField] private float spawnAreaHeight = 2.0f;
    [SerializeField] private int numWaves = 5;
    [SerializeField] private int numEnemiesPerWave = 10;
    [SerializeField] private float waveDuration = 20.0f;
    [SerializeField] private float timeToBoss = 20.0f;

    private bool waveEnded;
    private float currentWaveTime = 0f;
    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private int enemiesLeftInWave;

    public EnemyCategory TypeEnemy { get => typeEnemy; }
    public int CurrentWave { get => currentWave; }
    public int NumWaves { get => numWaves; }

    void Start()
    {     
        StartCoroutine(SpawnEnemies());
    }
    /*
    public bool isLastWave() 
    {
        return currentWave >= numWaves;
    }*/

    IEnumerator SpawnEnemies()
    {
        while (currentWave < numWaves)
        {
            enemiesLeftInWave = numEnemiesPerWave;
            waveEnded = false;

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
                enemiesLeftInWave--;

                yield return new WaitForSeconds(spawnDelay);
            }
            currentWave++;

            if (currentWave < numWaves)
            {              
                yield return new WaitForSeconds(waveDuration);
            }
            else if(currentWave >= numWaves) 
            {
                timeToBoss -= Time.deltaTime;
                yield return new WaitForSeconds(timeToBoss);
                Instantiate(bossPrefab, spawnPositionBoss.position, Quaternion.identity);              
            }

            waveEnded = true;

            if (enemiesLeftInWave <= 0 && waveEnded)
            {
                cityStatus.IncreaseHealth(10);
                playerStatus.IncreaseHealth(3);
            }
        }
    }
}