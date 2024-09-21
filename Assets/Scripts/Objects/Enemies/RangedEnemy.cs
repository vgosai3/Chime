using UnityEngine;
using UnityEngine.AI;

/// RangedEnemy maintains a distance from the player, 
/// retreating if too close and chasing if too far.
public class RangedEnemy : Enemy{
    [SerializeField]
    /// The movement speed of the Ranged enemy.
    private float MoveSpeed = 3.5f;

    [SerializeField]
    private float MinDistance = 5.0f;   // Minimum safe distance from player

    [SerializeField]
    private float MaxDistance = 15.0f;  // Maximum distance for chasing the player

    private NavMeshAgent _Agent;

    
    protected override void _Start(){

        // Initialize NavMeshAgent for movement
        _Agent = GetComponent<NavMeshAgent>();
        _Agent.speed = MoveSpeed;
    }


    void Update(){
        // Ensure player exists
        if (_Player != null){
            // Calculate distance from player
            float dist = Vector3.Distance(_Player.transform.position, transform.position);

            // If the player is too close, retreat
            if (dist < MinDistance) {
                Vector3 retreatDirection = (transform.position - _Player.transform.position).normalized;
                Vector3 retreatPosition = transform.position + retreatDirection * (MinDistance - dist);
                _Agent.SetDestination(retreatPosition);
            } else if (dist > MaxDistance){
                // If the player is too far, chase
                _Agent.SetDestination(_Player.transform.position);
            } else {
                // Stop moving if within the safe range
                _Agent.velocity = Vector3.zero;
            }
        }
    }

    /// Attack method defines the behavior of the RangedEnemy when attacking.
    /// Typically, this would involve shooting a projectile at the player.
    protected override void Attack(){
        Debug.Log("RangedEnemy attacks from distance!");
        // Example: Instantiate a projectile and fire toward the player
    }
}
