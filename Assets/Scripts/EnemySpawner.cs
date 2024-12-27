using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    protected GameObject _Player;

    private readonly float minDist = 5f;
    private readonly float maxDist = 10f;
    private readonly float spawnRate = 1f;
    private float _nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        _nextSpawnTime = Time.time;
        _Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Time.time > _nextSpawnTime)
        {
            _nextSpawnTime += spawnRate;
            int LayerMobs = LayerMask.NameToLayer("Mobs");
            //gameObject.layer = LayerMobs
            Instantiate(enemyPrefab, RandomPos(), Quaternion.identity);
        }*/
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < Math.Pow(2, Globals.dayCounter + 2) && !Globals.isDaytime)
        {
            var delta = UnityEngine.Random.insideUnitSphere * 4;
            var spawnPosition = _Player.transform.position + (delta.normalized * 10) + delta;
            spawnPosition.y = 0.5f;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
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
