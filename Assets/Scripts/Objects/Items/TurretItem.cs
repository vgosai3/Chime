using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItem : AItem
{
    public GameObject TurretPrefab;
    public override void PrimaryAction()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 2.0f;
        Instantiate(TurretPrefab, spawnPosition, Quaternion.identity);

    }
}
