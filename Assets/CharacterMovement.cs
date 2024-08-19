using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private readonly float MovementSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Forwards
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + new Vector3(0, 0, MovementSpeed);
        }
        // Backwards
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + new Vector3(0, 0, -MovementSpeed);
        }
        // Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + new Vector3(-MovementSpeed, 0);
        }
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(MovementSpeed, 0);
        }
        transform.rotation = Quaternion.LookRotation(transform.position);
    }
}
