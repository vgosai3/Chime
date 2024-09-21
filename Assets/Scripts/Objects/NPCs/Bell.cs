using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BellMovement : MonoBehaviour
{
    public Transform target;
    private readonly Vector3 Range = new Vector3(0f, 0f, -2f);
    public float followSharpness = 0.005f;

    private float FloatHeight = 1.0f; //Wenwei cheated and took away read only to do the hovering
    private readonly float FloatVariance = 0.2f;
    private readonly float FloatSpeed = 1.5f;

    private bool teleporting = false;
    private float pauseCount = 2f;

    // Start is called before the first frame updatew
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Floating animation
        transform.position =  new Vector3(transform.position.x, FloatVariance * Mathf.Sin(FloatSpeed * Time.time) + FloatHeight, transform.position.z);
    }

    private void LateUpdate()
    {
        // Follow character with delay
        if(!teleporting)
        {
            var magnitude = target.position + target.rotation.normalized * Range;
            transform.position += (magnitude - transform.position) * followSharpness;
        }//if teleporting, bell will not follow player
    }

    public void Hover(Transform Orb)
    {
        teleporting = true;
        FloatHeight += 2;
        transform.position = Orb.position;
        StartCoroutine(HoverAndWait());
    }//Hovering after orb interaction


    private IEnumerator HoverAndWait()
    {
        yield return new WaitForSeconds(pauseCount);
        teleporting = false;
        FloatHeight -= 2;
    }//waits pauseCount seconds before bell returns to normal

}
