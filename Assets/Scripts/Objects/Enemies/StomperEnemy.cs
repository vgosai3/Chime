using UnityEngine;
using UnityEngine.AI;

//It walks slowly towards theh player and attacks when within range
public class StomperEnemy : Enemy{
    [SerializeField]
    //The movement speed of the Stomper enemy
    private float MoveSpeed = 1.5f;

    public float Distance = 10.0f;

    private NavMeshAgent _Agent;

    protected override void _Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _Agent.speed = MoveSpeed;
    }

    void Update(){
        if(_Player != null){
            //Find the player's distance to the enemy
            float dist = Vector3.Distance(_Player.transform.position, transform.position);
            if (dist < Distance)
            {
                if (dist > AttackRadius)
                {
                    //Move towards player if the player is outside the attatck radius
                    _Agent.SetDestination(_Player.transform.position);
                }
                else
                {
                    //Set the velocity to zero to attack
                    _Agent.velocity = Vector3.zero;
                    /*Attack();*/
                }
            }
        }
    }

    //Attack logic from when within radius
    protected override void Attack(){
        //Define the attack method for the Stomper Enemy
        Debug.Log("StomperEnemy attacks!");
    }
}