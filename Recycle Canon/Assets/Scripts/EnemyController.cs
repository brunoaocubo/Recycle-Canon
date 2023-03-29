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
    public Transform targetCity; 
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

    void Start()
    {
        currentTarget = targetCity;
        rigidbody = GetComponent<Rigidbody>();
        targetCity = FindObjectOfType<City>().transform;
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
            currentTarget = targetCity;
            currentMoveSpeed = defaultSpeed;
        }

        Vector3 targetDirection = (currentTarget.position - transform.position).normalized;
        targetDirection.y = 0f;
        
        rigidbody.velocity = targetDirection.normalized * currentMoveSpeed;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    private void OnDrawGizmos()
    {
        Vector3 sphereCast = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.DrawSphere(sphereCast, sphereCastRadius);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            collision.collider.GetComponent<PlayerStatus>().TakeDamage(damagePlayer);
            Instantiate(trashDrops[(int)typeEnemy], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        else if(collision.gameObject.tag == "City") 
        {
            collision.collider.GetComponent<City>().TakeDamage(damageCity);
            Instantiate(trashDrops[(int)typeEnemy], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
