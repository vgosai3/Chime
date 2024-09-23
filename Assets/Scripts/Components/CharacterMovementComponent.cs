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

    public void MovePlayerRelativeToCamera(Vector3 movementVector, Vector3 directionToFace, Transform cameraTransform)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        //Relative Movement & Rotation
        Vector3 relativeMovement = (forward * movementVector.z + right * movementVector.x) * MovementSpeed;
        characterController.SimpleMove(relativeMovement);

        if (directionToFace != Vector3.zero)
        {
            directionToFace = new Vector3(relativeMovement.x, 0, relativeMovement.z);
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(directionToFace.x, 0, directionToFace.z), Vector3.up);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRot, RotationSpeed);
        }
    }
}
