using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : AItem
{
    [SerializeField] protected string WeaponName;
    [SerializeField] protected float HP = 100.0f;
    [SerializeField] protected float AttackCooldown = 0.5f;
    [SerializeField] protected float AttackSpeed = 0.5f;
    public float Damage = 100.0f;

    public void Start() 
    {
        GameObject _Enemy = GameObject.FindWithTag("Enemy");
    }
    

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