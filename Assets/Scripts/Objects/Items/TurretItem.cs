using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItem : AItem
{
    public GameObject TurretPrefab;
    public override void PrimaryAction()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 spawnPosition = player.transform.position + player.transform.forward * 2.0f;
        Instantiate(TurretPrefab, spawnPosition, Quaternion.identity);

    }
}
