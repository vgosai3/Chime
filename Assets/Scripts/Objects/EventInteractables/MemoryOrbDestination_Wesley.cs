using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MemoryOrbDestination_Wesley : AInteractableComponent
{
    public Transform memOrbReceiver;
    public GameObject itemPrefab;
    public GameObject currentObj;
    public GameObject partnerObj;

    public BellMovement bellMovement;
    public float pauseCount = 2f;
    public bool sender;

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
        if (sender)
        {
            SceneManager.LoadScene("Exposition Orb Layout", LoadSceneMode.Additive);
        }
        else
        {
            Instantiate(itemPrefab, memOrbReceiver.position + new Vector3(7, 0.5f , 0), Quaternion.identity);
        }
        yield return new WaitForSeconds(pauseCount);
        interactor.SetActive(false);
        interactor.transform.position = memOrbReceiver.position;
        interactor.SetActive(true);

        if (sender)
        {

            Globals.inOrb = true;
            //Hides the memory orb
            currentObj.SetActive(false);
        }
        else
        {
            Globals.inOrb = false;
            SceneManager.UnloadSceneAsync("Exposition Orb Layout");
            //Removes the memory orb and exit object from the game.
            Destroy(partnerObj);
            Destroy(currentObj);
        }

    }
}
