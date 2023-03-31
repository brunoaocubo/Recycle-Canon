using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject targetCity;
    [SerializeField] private GameObject[] trashDrops = new GameObject[3];
    private Transform currentTarget;
    private Rigidbody rigidbody;
    private EnemySpawner enemySpawner;

    [SerializeField] private float defaultSpeed;
    private float currentMoveSpeed;
    private float rotateSpeed = 10f;
    private bool isGrounded;

    [SerializeField] private float rangeDetected;
    [SerializeField] private int damageToPlayer;
    [SerializeField] private int damageToCity;
    [SerializeField] private float timeBetweenDamages = 1.0f;
    private float timeToNextDamage = 0.0f;

    [SerializeField] private float dropInterval = 0f;
    private float dropTimer = 0f;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //enemySpawner = FindObjectOfType<EnemySpawner>();
        currentTarget = targetCity.transform;
    }

    void Update()
    {
        BossMovement();

        float distanceToCity = Vector3.Distance(transform.position, targetCity.transform.position);
        dropTimer -= Time.deltaTime;

        if (distanceToCity > rangeDetected && transform.position.magnitude > 1)
        {
            if (dropTimer <= 0)
            {
                DropTrash();
                dropTimer = dropInterval;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void DropTrash()
    {
        int randomTrashIndex = Random.Range(0, trashDrops.Length);
        Vector3 dropPosition = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        Instantiate(trashDrops[randomTrashIndex], dropPosition, Quaternion.identity);
    }

    private void BossMovement() 
    {
        currentTarget = targetCity.transform;
        currentMoveSpeed = defaultSpeed;
        Vector3 targetDirection = (currentTarget.position - transform.position).normalized;
        targetDirection.y = 0f;

        rigidbody.velocity = targetDirection.normalized * currentMoveSpeed;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damageToPlayer);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("City"))
        {
            if (Time.time >= timeToNextDamage)
            {
                collision.gameObject.GetComponent<City>().TakeDamage(damageToCity);
                timeToNextDamage = Time.time + timeBetweenDamages;
            }
        }
    }
}
