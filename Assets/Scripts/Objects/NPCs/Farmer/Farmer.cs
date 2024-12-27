using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : NPC
{
    /// <summary>
    /// Dialogue asset for farmer dialogue.
    /// </summary>
    [SerializeField] public DialogueAsset FarmerDialogueAsset;

    // Start is called before the first frame update
    void Start()
    {
        this.canMove = false;
        this.id = (int)NonPlayerCharacters.Farmer;
    }

    public override void Interact(GameObject interactor)
    {
        Debug.Log(Globals.Player.IsInConversation);
        if (Globals.Player.IsInConversation)
        {
            UIController.Instance.SkipLine();
        }
        else
        {
            UIController.Instance.StartDialogue(FarmerDialogueAsset, StartDialoguePosition);
        }
    }
}
