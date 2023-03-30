using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeEnemy
{
    Organic,
    Plastic,
    Metal
}

public class EnemyController : MonoBehaviour
{
    public TypeEnemy typeEnemy;
    public GameObject[] trashDrops;
    private Transform currentTarget;
    public GameObject[] targetCity = new GameObject[3]; 
    public Transform targetPlayer;
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

    public float timer = 0;
    void Start()
    {
        randomIndexTarget = Random.Range(0, 3);
        currentTarget = targetCity[randomIndexTarget].transform;
        rigidbody = GetComponent<Rigidbody>();
        //targetCity = FindObjectOfType<City>().transform;
        targetPlayer = FindObjectOfType<Player>().transform;

        //typeEnemy = (TypeEnemy)Random.Range(0, 2);
    }

    void Update()
    {
        EnemyMovement();

        if (Physics.Raycast(transform.position, Vector3.down, 1f)) 
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
    private void FixedUpdate()
    {
        float distanceToCity = Vector3.Distance(transform.position, targetCity[randomIndexTarget].transform.position);
        
        if (distanceToCity >= rangeDetected)
        {
            timer -= Time.deltaTime;
            DropTrash();
        }
    }

    private void DropTrash() 
    {        
        if (timer <= 0)
        {
            Instantiate(trashDrops[(int)typeEnemy], transform.position, Quaternion.identity);
            timer = 6f;
        }       
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            collision.collider.GetComponent<PlayerStatus>().TakeDamage(damagePlayer);
            DropTrash();
            Destroy(this.gameObject);
        }

        else if(collision.gameObject.tag == "City") 
        {
            collision.collider.GetComponent<City>().TakeDamage(damageCity);
            DropTrash();
            Destroy(this.gameObject);
        }
    }
}
