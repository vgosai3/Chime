using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MemoryOrbDestination : AInteractableComponent
{
    public Transform memOrbReceiver;

    public BellMovement bellMovement;
    public float pauseCount = 2f;

    private bool interacting = false;
    public override void Interact(GameObject interactor)
    {
        if (!interacting)
        {
            interacting = true;//prevent spamming interact
            bellMovement.Hover(this.transform);
            StartCoroutine(WaitAndTeleport(interactor));
        }
    }
    private IEnumerator WaitAndTeleport(GameObject interactor)
    {
        yield return new WaitForSeconds(pauseCount);
        interactor.SetActive(false);
        interactor.transform.position = memOrbReceiver.position;
        interactor.SetActive(true);
        interacting = false;
    }
}
