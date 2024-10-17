using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// ChaserEnemy relentlessly chases the player for a set amount of time,
/// then rests for a few seconds before resuming the chase.
public class ChaserEnemy : Enemy
{
    public float Distance = 10.0f;

    [SerializeField]
    /// The movement speed of the Chaser enemy.
    private float MoveSpeed = 4.0f;

    [SerializeField]
    /// How long the enemy will chase the player before resting.
    private float ChaseDuration = 5.0f;

    [SerializeField]
    /// How long the enemy rests after chasing.
    private float RestDuration = 2.0f;

    private NavMeshAgent _Agent;
    private float _LastChaseTime;

    protected override void _Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _Agent.speed = MoveSpeed; //Sets theNavMeshAgent speed to the given move speed
        _LastChaseTime = Time.time; //Records the time that the lastChase started at
    }

    /// Update is called once per frame. ChaserEnemy runs toward the player
    /// for a fixed duration, then rests before repeating.
    void Update(){
        if (_Player != null) //player found
        {
            float dist = Vector3.Distance(_Player.transform.position, transform.position);
            if (dist < Distance)
            {
                // Chase the player for the chase duration, then rest for rest duration
                if (Time.time - _LastChaseTime < ChaseDuration) //If ((time at that point in the game) - (time last chase occurred) < ChaseTime)
                {
                    _Agent.SetDestination(_Player.transform.position);
                }
                else
                {
                    _Agent.velocity = Vector3.zero;
                    if (Time.time - _LastChaseTime >= ChaseDuration + RestDuration) // if sufficiently rested
                    {
                        _LastChaseTime = Time.time; //lastchase is now time elased in the game and then chase
                    }
                }
            }
            // Trigger collision-based attack
            /*if (dist < AttackRadius)
            {
                Attack();
            }*/
        }
    }

    /// Attack method is called when the ChaserEnemy collides with the player.
    protected override void Attack(){
        Debug.Log("ChaserEnemy attacks on collision!");
        // Example: Damage the player on collision
    }
}
