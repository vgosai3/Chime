using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTreeGenerator : MonoBehaviour 
{
    [Header("Prefabs and Terrain")]
    public GameObject[] prefabs; 
    public Terrain terrain; 

    [Header("Placement Settings")]
    public int numberOfObjects = 100; 
    public float minScale = 0.8f;
    public float maxScale = 1.5f;

    void Start()
    {
        PopulateTerrain();
    }

    void PopulateTerrain()
    {
        if (terrain == null || prefabs.Length == 0)
        {
            Debug.LogWarning("Terrain or prefabs not assigned!");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPosition = terrain.transform.position;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomX = Random.Range(0, terrainData.size.x);
            float randomZ = Random.Range(0, terrainData.size.z);

            float terrainHeight = terrainData.GetHeight((int)randomX, (int)randomZ);

            Vector3 position = new Vector3(
                randomX + terrainPosition.x,
                terrainHeight + terrainPosition.y,
                randomZ + terrainPosition.z
            );

            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

            GameObject spawnedObject = Instantiate(prefab, position, Quaternion.identity);

            float randomScale = Random.Range(minScale, maxScale);
            spawnedObject.transform.localScale = Vector3.one * randomScale;

            AlignToTerrainNormal(spawnedObject, position, terrainData);
        }

        Debug.Log($"{numberOfObjects} objects spawned on the terrain!");
    }

    void AlignToTerrainNormal(GameObject obj, Vector3 position, TerrainData terrainData)
    {
        Vector3 terrainNormal = terrainData.GetInterpolatedNormal(
            (position.x - terrain.transform.position.x) / terrainData.size.x,
            (position.z - terrain.transform.position.z) / terrainData.size.z
        );

        obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, terrainNormal);
    }
}
