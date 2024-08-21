using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BellMovement : MonoBehaviour
{
    public Transform target;
    private readonly Vector3 Range = new Vector3(0f, 0f, -2f);
    public float followSharpness = 0.05f;

    private readonly float FloatHeight = 1.0f;
    private readonly float FloatVariance = 0.2f;
    private readonly float FloatSpeed = 1.5f;

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
        var magnitude = target.position + target.rotation.normalized * Range;
        transform.position += (magnitude - transform.position) * followSharpness;
    }
}
