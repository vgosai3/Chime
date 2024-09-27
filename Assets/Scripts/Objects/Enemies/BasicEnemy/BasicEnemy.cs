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

    [Header("Bounce Settings")]
    public float BounceDistance = 2.0f; // The distance to bounce back
    public float BounceSpeed = 5.0f;    // The speed at which the enemy bounces back

    private bool isBouncingBack = false; // To check if the enemy is currently bouncing back
    private Vector3 bounceStartPosition; // The starting position of the bounce
    private Vector3 bounceTargetPosition;

    [Header("Hit Rate Settings")]
    public float hitRate = 2.0f;
    private float _lastHitTime;

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
                } else if (dist < AttackRadius) {
                    if (Time.time - _lastHitTime >= hitRate) {
                        Attack();
                        _lastHitTime = Time.time;

                        // Set up the bounce back, so when enemy attacks, player has time since enemy gets bounced back
                        isBouncingBack = true;
                        bounceStartPosition = transform.position;
                        bounceTargetPosition = transform.position - transform.forward * BounceDistance;
                    }
                }

                // Handle bounce-back movement
                if (isBouncingBack) {
                    // Move towards the bounce target position
                    transform.position = Vector3.MoveTowards(transform.position, bounceTargetPosition, BounceSpeed * Time.deltaTime);
                    // Check if the enemy has reached the bounce target position
                    if (Vector3.Distance(transform.position, bounceTargetPosition) < 0.01f) {
                        isBouncingBack = false; // Stop bouncing back once the target is reached
                    }
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
        // logging that player has been hit and how many points player has
        Debug.Log("Player hit by enemy." + _Player.GetComponent<Player>().HitPoints);
    }
}
