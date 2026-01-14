using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float damageAmount = 15f;

    public GameObject impactEffect;
    public GameObject damageEffect;

    public Rigidbody theRb;

    public float lifeTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        theRb.linearVelocity = transform.forward * moveSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            PlayerHealthContoller.Instance.TakeDamage(damageAmount);
        }
        else
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
