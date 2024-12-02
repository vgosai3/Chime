using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : AInteractableComponent
{
    [SerializeField] public DialogueAsset dialogueAsset;
    [SerializeField] public bool firstInteraction = true;
    [SerializeField] public int repeatStartPosition;

    protected DialogueBoxController dialogueController;

    protected GameObject _Player;
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

    // Start is called before the first frame update
    void Start()
    {
        dialogueController = DialogueBoxController.GetInstance();
        LateStart();
    }

    protected virtual void LateStart() { }

    // Update is called once per frame
    void Update()
    {
        _Player = GameObject.FindWithTag("Player");

        /*if (canMove)
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
        }*/

    }

    // For NPC -> enemy boss later on
    // protected abstract void Movement();

    //NPC Dialogue Based on Event Shifts
    // protected abstract void Interaction();
}
