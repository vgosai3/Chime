using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BellMovement : AInteractableComponent
{
    public Transform target;
    public AudioSource BellAudio;
    public float followSharpness = 0.05f;

    private readonly Vector3 Range = new Vector3(0f, 0f, -2f);

    [SerializeField]
    private float FloatHeight = 1.0f; //Wenwei cheated and took away read only to do the hovering
    [SerializeField]
    private float FloatVariance = 0.2f;
    [SerializeField]
    private float FloatSpeed = 3f;

    private bool Teleporting = false;
    public bool Following = false;
    private float pauseCount = 2f;

    // Start is called before the first frame updatew
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        // Follow character with delay
        if(Following && !Teleporting)
        {
            // Floating animation
            transform.position = new Vector3(transform.position.x, FloatVariance * Mathf.Sin(FloatSpeed * Time.time) + FloatHeight, transform.position.z);

            var magnitude = target.position + target.rotation.normalized * Range;
            transform.position += (magnitude - transform.position) * followSharpness;
        }//if teleporting, bell will not follow player
    }

    public void Hover(Transform Orb)
    {
        Teleporting = true;
        transform.position = Orb.position + (Vector3.up * 2.0f);
        StartCoroutine(HoverAndWait());
    }//Hovering after orb interaction


    private IEnumerator HoverAndWait()
    {
        yield return new WaitForSeconds(pauseCount);
        Teleporting = false;
    }//waits pauseCount seconds before bell returns to normal

    public override void Interact(GameObject interactor)
    {
        Debug.Log("Interacted with bell");
        Debug.Log(Following);
        Following = true;
        BellAudio.Play();
    }
}
