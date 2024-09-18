using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : AItem
{
    [SerializeField] protected string WeaponName;
    [SerializeField] protected float HP;
    [SerializeField] protected float AttackCooldown;
    [SerializeField] protected float AttackSpeed;
    [SerializeField] protected float Range;
    [SerializeField] protected float Damage;   

    public override void PrimaryAction() 
    {

    }

    public abstract void Attack();


    // if weapon gets hit then it takes damage
    // weapon will be destroyed if hp reaches 0
    public void WeaponTakeDamage(float damage) 
    {
        if (HP > damage) {
            HP -= damage;
        }
        else {
            Destroy(gameObject);
        }
            
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Enemy") {
            BasicEnemy enemy = other.gameObject.GetComponent<BasicEnemy>();
            enemy.TakeDamage(Damage, enemy.GetEnemyType());
            WeaponTakeDamage(enemy.Damage);
        }
    }



}