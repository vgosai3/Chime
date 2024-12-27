using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InteractorComponent : MonoBehaviour
{

    /// <summary>
    /// The collider used to determine interactions.
    /// </summary>
    public Collider InteractorArea;

    /// <summary>
    /// The maximum interactable distance.
    /// </summary>
    public float MaxDist = 100000.0f;

    /// <summary>
    /// Private list to maintain all interactable objects overlapping the interactor.
    /// </summary>
    private readonly List<AInteractableComponent> OverlappingInteractables = new();

    public void Reset()
    {
        // Attach new capsule collider to the current object if it does not exist
        if (InteractorArea == null)
        {
            InteractorArea = this.AddComponent<CapsuleCollider>();
        }

        // Set collider to trigger type if not already
        InteractorArea.isTrigger = true;
    }

    public void FixedUpdate()
    {
        // Clear list of overlapping interactable objects
        OverlappingInteractables.Clear();
    }

    public void OnTriggerStay(Collider other)
    {
        AInteractableComponent otherInteractableComponent = other.GetComponent<AInteractableComponent>();
        if (otherInteractableComponent != null)
        {
            OverlappingInteractables.Add(otherInteractableComponent);
        }

        
    }

    public void Interact()
    {
        AInteractableComponent closestInteractableComponent = null;
        var closeDist = MaxDist;

        foreach (AInteractableComponent currInteractableComponent in OverlappingInteractables)
        {
            float currDist = Vector3.Distance(currInteractableComponent.InteractableArea.transform.position, InteractorArea.transform.position);
            if (currDist < closeDist)
            {
                closestInteractableComponent = currInteractableComponent;
                closeDist = currDist;
            }
        }
        if (closestInteractableComponent != null)
        {
            closestInteractableComponent.Interact(this.gameObject);
        }
    }
}
