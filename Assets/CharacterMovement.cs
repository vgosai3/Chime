using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    private readonly float MovementSpeed = 5f;
    private readonly float RotationSpeed = 500f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new();

        // Forwards
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection.z += MovementSpeed;
        }
        // Backwards
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection.z -= MovementSpeed;
        }
        // Left
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x -= MovementSpeed;
        }
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x += MovementSpeed;
        }

        if (movementDirection != Vector3.zero)
        {
            transform.Translate(movementDirection * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(movementDirection, Vector3.up),
                RotationSpeed * Time.deltaTime);
        }
    }
}
