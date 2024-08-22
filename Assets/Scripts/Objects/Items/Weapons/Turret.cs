using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Bullet object
    public GameObject bullet;
    public float fireRate = 1.2f;
    private float _lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastFireTime >= fireRate)
        {
            GameObject closest = GetClosestEnemy();
            if (closest != null)
            {
                Rigidbody r = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                r.velocity = (closest.transform.position - transform.position).normalized * 10;
                _lastFireTime = Time.time;
            }
        }
    }

    private GameObject GetClosestEnemy()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        float closest = 1000;
        GameObject closestObject = null;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            float dist = Vector3.Distance(gameObjects[i].transform.position, transform.position);
            if (dist < closest)
            {
                closest = dist;
                closestObject = gameObjects[i];
            }
        }
        return closestObject;
    }
}
