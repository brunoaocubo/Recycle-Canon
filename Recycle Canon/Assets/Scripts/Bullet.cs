using UnityEngine;

public enum TypeBullet 
{
    Organic,
    Plastic,
    Metal
}

public class Bullet : MonoBehaviour
{
    private Canon canon;
    private EnemyStatus enemyStatus;
    private float bulletSpeed = 20f;
    private int damage = 1;

    private void Start()
    {
        canon = FindObjectOfType<Canon>();
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (canon.ammoType)
        {
            case AmmoType.Organic:
                if(collision.collider.CompareTag("EnemyPlastic") || collision.collider.CompareTag("EnemyMetal") || collision.collider.CompareTag("Boss")) 
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamage(damage);
                    Destroy(gameObject);
                }

                if (collision.collider.CompareTag("Boss"))
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamageBoss(damage);
                    Destroy(gameObject);
                }
                break;

            case AmmoType.Plastic:
                if (collision.collider.CompareTag("EnemyOrganic"))
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamage(damage);
                    Destroy(gameObject);
                }

                if (collision.collider.CompareTag("Boss")) 
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamageBoss(damage);
                    Destroy(gameObject);
                }
                break;

            case AmmoType.Metal:
                if (collision.collider.CompareTag("EnemyOrganic") || collision.collider.CompareTag("Boss"))
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamage(damage);
                    Destroy(gameObject);
                }

                if (collision.collider.CompareTag("Boss"))
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamageBoss(damage);
                    Destroy(gameObject);
                }
                break;
        }
    }
}
