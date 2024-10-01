using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

/// LeaperEnemy jumps toward the player, leaping onto them when in attack range.
public class LeaperEnemy : Enemy
{
    public float Distance = 10.0f;

    [SerializeField]
    /// The leap distance.
    private float LeapDistance = 8.0f;

    [SerializeField]
    /// Cooldown time between leaps.
    private float LeapCooldown = 2.0f;

    [SerializeField]
    /// The height of the leap.
    private float LeapHeight = 2.0f;

    [SerializeField]
    /// The duration of the leap.
    private float LeapDuration = 1.0f;

    private float _LastLeapTime;

    private bool _IsLeaping = false;
    private Vector3 _LeapTarget;
    private NavMeshAgent _Agent;

    protected override void _Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _LastLeapTime = Time.time;
    }

    void Update(){
        if (_Player != null)
        {
            float dist = Vector3.Distance(_Player.transform.position, transform.position);
            if (dist < Distance)
            {
                if (!_IsLeaping)
                {
                    // Leap toward the player if within leap distance and enough time has passed
                    if (dist <= LeapDistance && Time.time - _LastLeapTime >= LeapCooldown)
                    {
                        StartLeap();
                    }
                }
                else
                {
                    // Check if the leap is complete
                    CompleteLeapCheck();
                }
            }
            // Stop moving and attack if within attack range
            /*if (dist <= AttackRadius)
            {
                Attack();
            }*/
        }
    }

    /// Initializes the leap movement.
    private void StartLeap(){
        _IsLeaping = true;
        _LeapTarget = _Player.transform.position; // Set the leap target to the player's position
        _Agent.destination = _LeapTarget;
        _Agent.speed *= 2; // Increase speed for the leap
        _LastLeapTime = Time.time;

        // Start the vertical movement coroutine
        StartCoroutine(LeapCoroutine());
        Debug.Log("LeaperEnemy starts leaping!");
    }

    private void CompleteLeapCheck(){
        if (!_Agent.pathPending && _Agent.remainingDistance <= _Agent.stoppingDistance){
            _IsLeaping = false;
            _Agent.speed /= 2; // Reset speed after leap
            Debug.Log("LeaperEnemy completes leap!");
        }
    }

    /// Coroutine to handle vertical movement during the leap.
    private IEnumerator LeapCoroutine(){
        float elapsedTime = 0f;
        Vector3 startPosition = _Agent.transform.position;
        Vector3 targetPosition = _LeapTarget;
        Vector3 peakPosition = startPosition + Vector3.up * LeapHeight;

        while (elapsedTime < LeapDuration)
        {
            float t = elapsedTime / LeapDuration;
            // Parabolic movement
            _Agent.transform.position = Vector3.Lerp(startPosition, peakPosition, t) + Vector3.Lerp(peakPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is the target position
        _Agent.transform.position = targetPosition;
    }

    /// Attack method is triggered when the enemy lands within range of the player.
    protected override void Attack()
    {
        Debug.Log("LeaperEnemy attacks after leaping!");
        // Example: Damage the player after landing on them
    }
}
