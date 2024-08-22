using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Set time 
    private readonly float timeAlive = 3f;
    private float _spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive < Time.time - _spawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
