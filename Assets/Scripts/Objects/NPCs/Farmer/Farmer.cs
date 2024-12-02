using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : NPC
{

    // Start is called before the first frame update
    protected override void LateStart()
    {
        this.canMove = false;
        this.id = (int)NonPlayerCharacters.Farmer;
    }

    public override void Interact(GameObject interactor)
    {
        var script = _Player.GetComponent<Player>();
        if (script.inConversation)
        {
            dialogueController.SkipLine();
        }
        else
        {
            dialogueController.StartDialogue(dialogueAsset, StartDialoguePosition);
        }
    }

    //NPC Dialogue Based on Event Shifts
    /*protected override void Interaction()
    {
        Debug.Log("Hi, I'm Vishnu. I love OSU and bells. And Belle. I didn't say that.");
    }

    protected override void Movement()
    {
        Debug.Log("Vishu can't move :(");
    }*/
}
