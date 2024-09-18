using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon
{
    private Animator animator;
    private bool CanAttack = true;

    static readonly int AttackingHash = Animator.StringToHash("Attacking");
    private object Lock = new();

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
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
