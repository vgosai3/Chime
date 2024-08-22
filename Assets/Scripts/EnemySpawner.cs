using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private readonly float minDist = 5f;
    private readonly float maxDist = 10f;
    private readonly float spawnRate = 1f;
    private float _nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        _nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawnTime)
        {
            _nextSpawnTime += spawnRate;
            int LayerMobs = LayerMask.NameToLayer("Mobs");
            //gameObject.layer = LayerMobs
            Instantiate(enemyPrefab, RandomPos(), Quaternion.identity);
        }

    }

    private Vector3 RandomPos()
    {
        double a = UnityEngine.Random.Range(0, 120f);
        double angle = a * Math.PI / 180f;
        double distance = UnityEngine.Random.Range(minDist, maxDist);
        return new Vector3((float)(Math.Cos(angle) * distance), 0.5f, (float)(Math.Sin(angle) * distance));
    }

}
