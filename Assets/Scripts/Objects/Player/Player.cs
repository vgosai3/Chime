using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public GameObject TurretPrefab;

    private readonly float MovementSpeed = 5f;
    private readonly float RotationSpeed = 500f;
    private bool Placeable = false;

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

        if (Input.GetMouseButtonDown(1))
        {
            TryPlaceTurret();
        }
    }

    private void TryPlaceTurret()
    {
        if (Placeable)
        {
            Instantiate(TurretPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            Placeable = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            Placeable = false;
        }
    }
}
