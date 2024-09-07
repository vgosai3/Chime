using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : AItem
{
    public float Damage;
    public Collider HitBox;

    public override void Reset()
    {
        base.Reset();
        HitBox = base.gameObject.AddComponent<BoxCollider>();
    }

    public override void PrimaryAction()
    {
        Debug.Log(this.name + " attack started!");
    }
}
