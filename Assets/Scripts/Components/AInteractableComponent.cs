using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AInteractableComponent : MonoBehaviour
{
    public Collider InteractableArea;
    public virtual void Reset()
    {
        InteractableArea = this.GetComponent<Collider>();
        InteractableArea.isTrigger = true;
        Rigidbody rb = this.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }
    public virtual void Interact(GameObject interactor)
    {
        Debug.Log("Successful interaction");
    }
}
