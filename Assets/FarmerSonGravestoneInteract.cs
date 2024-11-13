using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerSonGravestoneInteract : AInteractableComponent
{
    public GameObject InteractText;
    public float TimeAlive = 4.0f;

    private float _lastInteractTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(GameObject interactor)
    {
        if (Time.time - _lastInteractTime > TimeAlive)
        {
            // Create new damage text object above enemy position
            GameObject interactText = Instantiate(InteractText, transform.position + Vector3.up * 5, Constants.GetCameraAngles());
            FloatingText textScript = interactText.GetComponent<FloatingText>();
            textScript.timeAlive = TimeAlive;
            textScript.Delta = 0.03f;
            textScript.Text = "Here lies the farmer's son.\n What a shame.";
            textScript.FontSize = 4.0f;
            _lastInteractTime = Time.time;
        }
    }
}
