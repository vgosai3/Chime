using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;

    static readonly int AttackingHash = Animator.StringToHash("Attacking");
    private object Lock = new();

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

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
            Attack();
        }
    }

    void Attack()
    {
        if (!animator.GetBool(AttackingHash))
        {
            lock (Lock)
            {
                if (!animator.GetBool(AttackingHash))
                {
                    animator.enabled = true;
                    animator.Play("SwordSwing");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator.GetBool(AttackingHash) && other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
