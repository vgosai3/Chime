using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Describe how to effectively use this class

public abstract class AItem : AInteractableComponent
{
    public Sprite ItemIcon;
    private bool inInventory;
    private MeshRenderer meshRenderer;

    public override void Reset()
    {
        base.Reset();
        this.AddComponent<MeshFilter>();
        this.AddComponent<MeshRenderer>();
    }
    public void Start()
    {
        inInventory = false;
    }
    public override void Interact(GameObject interactor)
    {
        PlayerInventoryComponent interactorInventory = interactor.GetComponent<PlayerInventoryComponent>();
        if (interactorInventory != null && inInventory == false)
        {
            PickUp(interactorInventory);
        }
    }

    public virtual void PickUp(PlayerInventoryComponent interactorInventory)
    {
        interactorInventory.AddItem(this);
        inInventory = true;
        this.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log(this.name + " has been picked up.");
    }
    public virtual void Drop(Vector3 dropPosition)
    {
        this.transform.position = dropPosition;
        this.GetComponent<MeshRenderer>().enabled = true;
        this.inInventory = false;
        //Add drop animation
    }
    public virtual void Equip()
    {
        //Move to position it will be held
        //Start rendering again
        Debug.Log(this.name + " has been equipped.");
    }
    public virtual void Unequip()
    {
        //Stop rendering
        Debug.Log(this.name + " has been unequipped.");
    }
    public abstract void PrimaryAction();
}
