using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : AInteractableComponent
{
    [SerializeField] public bool firstInteraction = true;
    [SerializeField] public int repeatStartPosition;

    protected bool canMove = false;
    protected const double INTERACT_RADIUS = 8;
    protected int id = -1;
    public string npcName;

    //returns where the dialogue should start
    public int StartDialoguePosition 
    {
        get 
        {
            if (firstInteraction) 
            {
                firstInteraction = false;
                return 0;
            }
            else 
            {
                return repeatStartPosition;
            }
        }
    }

    // For NPC -> enemy boss later on
    // protected abstract void Movement();
}
