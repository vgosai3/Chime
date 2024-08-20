using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public Transform target;
    private Animator animator;

    static readonly int AttackingHash = Animator.StringToHash("Attacking");
    private readonly int AnimationLayerHash = Animator.StringToHash("Base Layer");
    private readonly Vector3 offset = new(-1, 0, 0);
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
        print("Update");
        print(animator.isActiveAndEnabled);
        print(animator.IsInTransition(AnimationLayerHash));
        // Check if attack animation is playing before readjusting the position and rotation of the weapon
        if (!animator.isActiveAndEnabled)
        {
            transform.position = target.position + (target.rotation.normalized * Quaternion.Euler(0.0f, 0.0f, 0.0f)) * offset;
            transform.rotation = target.rotation * Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }

        // On left click, call attack function
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        print("Called");
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
}
