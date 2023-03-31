using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
   
    [SerializeField] private GameObject[] targetCity = new GameObject[3];
    [SerializeField] private GameObject[] trashDrops = new GameObject[3];
    private Transform targetPlayer;
    private Transform currentTarget;
    private Rigidbody rigidbody;
    private EnemySpawner enemySpawner;

    [SerializeField] private float defaultSpeed;
    [SerializeField] private float bonusSpeed;
    private float currentMoveSpeed;
    private float rotateSpeed = 10f;
    private bool isGrounded;

    [SerializeField] private float rangeDetected;
    [SerializeField] private int damageToPlayer;
    [SerializeField] private int damageToCity;

    [SerializeField] private float dropInterval = 3f;
    private float timerToDrop;

    private int randomIndexTarget;
    private bool canDamage = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        targetPlayer = FindObjectOfType<Player>().transform;

        currentTarget = targetCity[randomIndexTarget].transform;
        randomIndexTarget = Random.Range(0, 3);
    }

    void Update()
    {
        EnemyMovement();
        RandomDropWithMove();

        if (Physics.Raycast(transform.position, Vector3.down)) 
        {
            isGrounded = true;
        }
        else 
        {
            isGrounded = false;
        }
    }

    private void EnemyMovement() 
    {    
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
        if (distanceToPlayer < rangeDetected)
        {
            currentTarget = targetPlayer;
            currentMoveSpeed = bonusSpeed;
        }
        else if (distanceToPlayer > rangeDetected)
        {
            currentTarget = targetCity[randomIndexTarget].transform;
            currentMoveSpeed = defaultSpeed;
        }

        Vector3 targetDirection = (currentTarget.position - transform.position).normalized;
        targetDirection.y = 0f;
        
        rigidbody.velocity = targetDirection.normalized * currentMoveSpeed;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    private void RandomDropWithMove() 
    {
        float distanceToCity = Vector3.Distance(transform.position, targetCity[randomIndexTarget].transform.position);
        timerToDrop -= Time.deltaTime;

        if (distanceToCity > rangeDetected && transform.position.magnitude > 0)
        {
            if (timerToDrop <= 0)
            {
                DropTrash();
                timerToDrop = dropInterval;
            }
        }
    }

    private void DropTrash() 
    {                
        Instantiate(trashDrops[(int)enemySpawner.TypeEnemy], transform.position, Quaternion.identity);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Player") && canDamage) 
        {
            DropTrash();
            canDamage = false;
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damageToPlayer);
            Destroy(gameObject);
        }
        else if(collision.collider.gameObject.CompareTag("City")) 
        {         
            collision.gameObject.GetComponent<City>().TakeDamage(damageToCity);
            DropTrash();
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("City")) 
        {
            canDamage = true;
        }
    }
}
