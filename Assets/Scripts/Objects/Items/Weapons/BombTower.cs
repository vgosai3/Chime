using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    public GameObject bombProjectilePrefab;
    public float fireRate = 2.0f;
    private float _lastFireTime;

    void Update()
    {
        if (Time.time - _lastFireTime >= fireRate)
        {
            GameObject closestEnemy = GetClosestEnemy();
            if (closestEnemy != null)
            {
                LaunchBombProjectile(closestEnemy);
                _lastFireTime = Time.time;
            }
        }
    }

    private void LaunchBombProjectile(GameObject target)
    {
        GameObject bombProjectile = Instantiate(bombProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = bombProjectile.GetComponent<Rigidbody>();

        Vector3 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * 10f; 
    }

    private GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
