using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public float MovementSpeed = 3.0f;
    public float Distance = 10.0f;

    private GameObject player;
    private Rigidbody rb;
    /*bool lookat = false;*/


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PlayerDetection.found)
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
        }*/
        if (player != null)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < Distance)
            {
                transform.LookAt(player.transform);
                /*Vector3 vel = rb.velocity; //Player speed use to limit speed of enemies so they dont run down player for now*/
                /*if (vel.x > -2 && vel.x < 2 && vel.z > -2 && vel.z < 2)
                {
                    rb.AddForce(MovementSpeed * Multiplier * Time.deltaTime * transform.forward);
                }*/
                transform.Translate(MovementSpeed * Vector3.forward * Time.deltaTime);
            }
        } else
        {
            player = GameObject.FindWithTag("Player");
        }
    }
}
