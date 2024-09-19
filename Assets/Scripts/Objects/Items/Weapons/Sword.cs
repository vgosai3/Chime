using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon
{
    private Animator animator;

    // placeholder stats, can change later
    private void Awake()
    {
        WeaponName = "Sword";
        Damage = 100;
        Range = 1.5f;
        AttackSpeed = 1.2f;
        MaxHP = 100.0f;
        HP = MaxHP;
        animator = gameObject.GetComponent<Animator>();
    }

    public override void Attack()
    {
        // to be implemented
        
    }
}
