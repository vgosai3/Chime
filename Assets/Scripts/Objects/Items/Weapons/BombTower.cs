using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    public GameObject bombProjectilePrefab;
    public float speed = 3.0f;
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

        // Calculate distance from bomb tower to enemy
        var distance = Vector3.Distance(target.transform.position, transform.position);
        // Calculate time to impact based on horizontal speed
        var timeOfImpact = distance / speed;
        // Calculate direction of horizontal movement
        Vector3 direction = (target.transform.position - transform.position).normalized;
        // Calculate vertical velocity required to return to surface on time of impact
        var verticalVelocity =
            Vector3.up
            * ((0.5f
            * Physics.gravity.magnitude
            * timeOfImpact
            * timeOfImpact)
            / timeOfImpact);
        // Assign horizontal and vertical velocity to rigidbody
        rb.velocity = (direction * speed) + verticalVelocity;
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
