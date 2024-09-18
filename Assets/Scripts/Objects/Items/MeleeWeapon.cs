using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : AItem
{
    [SerializeField] protected string WeaponName;
    [SerializeField] protected int Damage;
    [SerializeField] protected float Range;
    [SerializeField] protected float AttackSpeed = 1.0f;
    [SerializeField] protected float HP = 100.0f;
    [SerializeField] protected float AttackCooldown = 0.5f;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

    public override void PrimaryAction() 
    {

    }

    public abstract void Attack();


    // if weapon gets hit then it takes damage
    // weapon will be destroyed if hp reaches 0
    public void TakeDamage(float damage) 
    {
        if (hp > damage) {
            hp -= damage;
        }
        else {
            Destroy(gameObject);
        }
            
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Enemy") {
            other.TakeDamage(Damage);
        }
    }



}