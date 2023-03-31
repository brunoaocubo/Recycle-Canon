using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    private Transform currentTarget;
    [SerializeField] private GameObject[] targetCity = new GameObject[3];
    [SerializeField] private GameObject[] trashDrops = new GameObject[3];
    private EnemySpawner enemySpawner;
    [SerializeField] private Transform targetPlayer;
    private Rigidbody rigidbody;

    private float currentMoveSpeed;
    public float defaultSpeed;
    public float bonusSpeed;
    private float rotateSpeed = 10f;
    public bool isGrounded;

    public float rangeDetected; 
    public int damagePlayer; 
    public int damageCity;

    public float sphereCastRadius = 0.7f;
    public int randomIndexTarget;

    private float timer;
    public float timerToDrop = 3f;

    private bool canDamage = true;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        targetPlayer = FindObjectOfType<Player>().transform;
        currentTarget = targetCity[randomIndexTarget].transform;
        randomIndexTarget = Random.Range(0, 3);
        enemySpawner = FindObjectOfType<EnemySpawner>();
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
        timer -= Time.deltaTime;

        if (distanceToCity > rangeDetected && transform.position.magnitude > 0)
        {
            if (timer <= 0)
            {
                DropTrash();
                timer = timerToDrop;
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
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damagePlayer);
            Destroy(gameObject);
        }
        else if(collision.collider.gameObject.CompareTag("City")) 
        {         
            collision.gameObject.GetComponent<City>().TakeDamage(damageCity);
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
