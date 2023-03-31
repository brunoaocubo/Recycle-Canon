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

        /*
        switch (gameObject.tag)
        {
            case "BulletOrganic":
                typeBullet = TypeBullet.Organic;
                break;
            case "BulletPlastic":
                typeBullet = TypeBullet.Plastic;
                break;
            case "BulletMetal":
                typeBullet = TypeBullet.Metal;
                break;
        }*/
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
                if(collision.collider.CompareTag("EnemyPlastic") || collision.collider.CompareTag("EnemyMetal")) 
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                break;
            case AmmoType.Plastic:
                if (collision.collider.CompareTag("EnemyOrganic"))
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamage(damage);
                    Destroy(gameObject);

                }
                break;
            case AmmoType.Metal:
                if (collision.collider.CompareTag("EnemyOrganic"))
                {
                    collision.collider.GetComponent<EnemyStatus>().TakeDamage(damage);
                    Destroy(gameObject);

                }
                break;
        }
    }
}
