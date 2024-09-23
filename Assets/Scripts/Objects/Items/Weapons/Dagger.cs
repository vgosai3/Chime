using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MeleeWeapon 
{
    private Animator animator;

    // placeholder stats, can change later
    private void Awake()
    {
        WeaponName = "Dagger";
        Damage = 50;
        Range = 0.5f;
        AttackSpeed = 5.0f;
        MaxHP = 100.0f;
        HP = MaxHP;
        animator = gameObject.GetComponent<Animator>();
    }

    public override void Attack()
    {
        // to be implemented
    }

    
}