using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject[] trashDrops = new GameObject[3];
    private EnemySpawner enemySpawner;

    public float dropInterval = 3f;
    private float dropTimer = 0f;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        dropTimer += Time.deltaTime;
        if (dropTimer >= dropInterval)
        {
            DropTrash();
            dropTimer = 0f;
        }
    }

    private void DropTrash()
    {
        int randomTrashIndex = Random.Range(0, trashDrops.Length);
        Vector3 dropPosition = transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
        Instantiate(trashDrops[randomTrashIndex], dropPosition, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            DropTrash();
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(1);
        }
        else if (collision.collider.gameObject.CompareTag("City"))
        {
            DropTrash();
            collision.gameObject.GetComponent<City>().TakeDamage(1);
        }
    }
}
