using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public float Damage = 3.0f;

    [Header("Movement Settings")]
    public float MovementSpeed = 3.0f;
    public float Distance = 10.0f;

    // Update is called once per frame
    void Update()
    {

        if (_Player != null)
        {
            float dist = Vector3.Distance(_Player.transform.position, transform.position);
            if (dist < Distance)
            {
                transform.LookAt(_Player.transform);
                if (dist >= AttackRadius)
                {
                    transform.Translate(MovementSpeed * Vector3.forward * Time.deltaTime);
                }
            }
        } else
        {
            _Player = GameObject.FindWithTag("Player");
        }
    }

    protected override void Attack()
    {
        // Get player script
        var script = _Player.GetComponent<Player>();
        script.TakeDamage(Damage);
    }
}
