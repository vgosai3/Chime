using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MemoryOrbDestination_Wesley : AInteractableComponent
{
    public Transform memOrbReceiver;
    public GameObject currentNPC;
    public GameObject currentObj;
    public GameObject partnerObj;

    public BellMovement bellMovement;
    public float pauseCount = 2f;
    public bool sender;

    private bool interacting = false;
    public override void Interact(GameObject interactor)
    {
        if (!interacting && bellMovement.Following)
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
        // interactor.transform.position = memOrbReceiver.position;
        interactor.SetActive(true);
        partnerObj.SetActive(true);

        if (sender)
        {
            //Spawns new NPC.
            /* Instantiate(currentNPC, transform.position + new Vector3(7, -0.5f, 0), Quaternion.identity);*/
            SceneManager.LoadSceneAsync("Farmer Memory Orb");
            // Reset timescale if returned from pause menu
            Time.timeScale = 1f;
            Globals.inOrb = true;
            //Hides the memory orb
            currentObj.SetActive(false);
        }
        else
        {
            SceneManager.LoadSceneAsync("Level");
            // Reset timescale if returned from pause menu
            Time.timeScale = 1f;
            Globals.inOrb = false;
            //Removes the memory orb and exit object from the game.
            Destroy(partnerObj);
            Destroy(currentObj);
        }

    }
}
