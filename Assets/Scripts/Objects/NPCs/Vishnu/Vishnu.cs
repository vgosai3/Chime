using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vishnu : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        this.canMove = false;
    }

    //NPC Dialogue Based on Event Shifts
    protected override void Interaction()
    {
        Debug.Log("Hi, I'm Vishnu. I love OSU and bells. And Belle. I didn't say that.");
    }

    protected override void Movement()
    {
        Debug.Log("Vishu can't move :(");
    }
}
