using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// DasherEnemy dashes toward the player, passing through
/// them and attacking after each dash.
public class DasherEnemy : Enemy
{
    [SerializeField]
    /// The distance covered by each dash.
    private float DashDistance = 10.0f;

    [SerializeField]
    /// The cooldown between dashes.
    private float DashCooldown = 3.0f;

    [SerializeField]
    /// The speed multiplier during the dash.
    private float DashSpeedMultiplier = 2.0f;

    public float Distance = 10.0f;

    private float _LastDashTime;
    private bool _IsDashing = false;
    private Vector3 _DashDirection;
    private NavMeshAgent _Agent;

    protected override void _Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _LastDashTime = Time.time;
    }

    void Update(){
        if (_Player != null)
        {
            float dist = Vector3.Distance(_Player.transform.position, transform.position);
            if (dist < Distance)
            {
                if (!_IsDashing) //if not dashing
                {
                    // Dash toward the player if within range and enough time has passed since the last dash
                    if (Time.time - _LastDashTime >= DashCooldown)
                    {
                        StartDash();
                    }
                }
                else
                {
                    // Continue dashing
                    PerformDash();
                }
            }

            // Stop when close to the player to attack
            /*if (dist < AttackRadius)
            {
                Attack();
            }*/
        }
    }

    /// Initializes the dashing movement.
    private void StartDash(){
        _IsDashing = true;
        _DashDirection = (_Player.transform.position - transform.position).normalized;
        _Agent.speed *= DashSpeedMultiplier;  // Increase speed for dashing
        _Agent.destination = transform.position + _DashDirection * DashDistance; // Set dash destination
        _LastDashTime = Time.time;
        Debug.Log("DasherEnemy starts dashing!");
    }

    /// Performs the dash movement.
    private void PerformDash(){
        // Check if the dash is complete by checking if the agent has reached the dash destination
        if (!_Agent.pathPending && _Agent.remainingDistance <= _Agent.stoppingDistance){
            _IsDashing = false;
            _Agent.speed /= DashSpeedMultiplier; // Resets speed
            Debug.Log("DasherEnemy completes dash!");
        }
    }

    /// Attack method is triggered 
    protected override void Attack(){
        Debug.Log("DasherEnemy attacks after dash!");
        // Example: Damage the player
    }
}
