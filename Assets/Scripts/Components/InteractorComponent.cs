using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InteractorComponent : MonoBehaviour
{
    public Collider InteractorArea;
    private List<AInteractableComponent> overlappingInteractables = new List<AInteractableComponent>();
    public void Reset()
    {
        InteractorArea = this.AddComponent<CapsuleCollider>();
        //Apply offset infront of player
        InteractorArea.isTrigger = true;
    }
    public void FixedUpdate()
    {
        overlappingInteractables.Clear();
    }
    public void OnTriggerStay(Collider other)
    {
        AInteractableComponent otherInteractableComponent = other.GetComponent<AInteractableComponent>();
        if (otherInteractableComponent != null)
        {
            overlappingInteractables.Add(otherInteractableComponent);
        }

        
    }

    public void Interact()
    {
        AInteractableComponent closestInteractableComponent = null;
        float closestDist = 100000.0f;
        foreach (AInteractableComponent currInteractableComponent in overlappingInteractables)
        {
            float currDist = Vector3.Distance(currInteractableComponent.InteractableArea.transform.position, InteractorArea.transform.position);
            if (currDist < closestDist)
            {
                closestInteractableComponent = currInteractableComponent;
                closestDist = currDist;
            }
        }

        if (closestInteractableComponent != null)
        {
            closestInteractableComponent.Interact(this.gameObject);
        }
    }
}
