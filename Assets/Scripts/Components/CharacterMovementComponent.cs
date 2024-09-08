using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovementComponent : MonoBehaviour
{
    private CharacterController characterController;
    public float MovementSpeed = 5.0f;
    public float RotationSpeed = 0.2f;
    public void Reset()
    {
        this.AddComponent<CharacterController>();
    }
    public void Start()
    {
        characterController = this.GetComponent<CharacterController>();
    }
    public void MoveCharacter(Vector3 movementVector, Vector3 directionToFace)
    {
        Vector3 scaledMovementVector = movementVector * MovementSpeed;
        characterController.SimpleMove(scaledMovementVector);
    

        if (directionToFace != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(directionToFace.x, 0, directionToFace.z), Vector3.up);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRot, RotationSpeed);
        }
    }
}
