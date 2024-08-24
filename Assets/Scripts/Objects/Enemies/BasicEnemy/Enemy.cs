using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly float MovementSpeed = 5f;
    bool lookat = false;
    public GameObject player;
    Rigidbody rb;
    private readonly float multiplier = 10;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetection.found)
        {
            lookat = true;
        }
        if (lookat)
        {
            transform.LookAt(player.transform);
            Vector3 vel = rb.velocity; //Player speed use to limit speed of enemies so they dont run down player for now
            if (!PlayerDetection.found && vel.x > -2 && vel.x < 2 && vel.z > -2 && vel.z < 2) 
            {
                rb.AddForce(MovementSpeed * multiplier * Time.deltaTime * transform.forward);
            }
        }
    }
}
