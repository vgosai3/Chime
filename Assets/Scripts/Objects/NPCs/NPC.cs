using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    protected GameObject _Player;
    protected bool canMove = false;
    protected const double INTERACT_RADIUS = 4;
    protected int id = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _Player = GameObject.FindWithTag("Player");

        if (canMove)
        {
            Movement();
        }

        //when E is pressed and player in radius, interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(transform.position, _Player.transform.position) <= INTERACT_RADIUS)
            {
                Interaction();
            }
        }

    }

    // For NPC -> enemy boss later on
    protected abstract void Movement();

    //NPC Dialogue Based on Event Shifts
    protected abstract void Interaction();
}
