using UnityEngine;
using System.Collections;

public class BombProjectile : MonoBehaviour
{
    public float explosionRadius = 30.0f;
    public float damageAmount = 50.0f;
    private readonly float timeAlive = 5f;
    private float _spawnTime;

    void Start()
    {
        _spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - _spawnTime >= timeAlive)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Debug.Log("Is floor");
        }
        if (other.CompareTag("Enemy") || other.CompareTag("Floor"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            //Debug.Log(nearbyObject.tag);
            if (nearbyObject.CompareTag("Enemy"))
            {
                Enemy enemy = nearbyObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount, Type.None);
                }
            }
        }
        Debug.Log("Destroy Projectile");
        Destroy(gameObject);
    }
}
