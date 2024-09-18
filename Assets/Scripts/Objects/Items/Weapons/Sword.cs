using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon
{
    private Animator animator;
    
    private bool CanAttack = true;

    static readonly int AttackingHash = Animator.StringToHash("Attacking");
    private object Lock = new();

    private void Awake()
    {
        WeaponName = "Sword";
        Damage = 100;
        Range = 1.5f;
        AttackSpeed = 1.2f;
        MaxHP = 100.0f;
        HP = MaxHP;
        AttackCooldown = 1.0f;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.enabled = false;
        if (!animator.GetBool(AttackingHash))
        {
            transform.position = transform.parent.transform.position;
        }
        // On left click, call attack function
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack) 
            {
                Attack();
            }
        }
    }

    public override void Attack()
    {
        CanAttack = false;
        if (!animator.GetBool(AttackingHash))
        {
            lock (Lock)
            {
                if (!animator.GetBool(AttackingHash))
                {
                    animator.enabled = true;
                    animator.Play("SwordSwing");
                    StartCoroutine(ResetAttackCooldown());
                }
            }
        }
        
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
}
