using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovementComponent : MonoBehaviour
{
    private CharacterController characterController;
    public float MovementSpeed = 5.0f;
    public float RotationSpeed = 0.2f;

    // Dash mechanics
    [SerializeField] private float DashSpeed = 10.0f;
    [SerializeField] private float DashDuration = 0.4f;
    [SerializeField] public float DashCooldown = 3.0f;
    private bool IsDashing = false;
    public float LastDash = 0.0f;

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
        if (!IsDashing)
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

    public IEnumerator PlayerDash()
    {
        Debug.Log("Dash Coroutine Start");
        if (!IsDashing && DeltaCurrentTime(LastDash) > DashCooldown)
        {
            IsDashing = true;
            yield return StartCoroutine(DashCoroutine());
            IsDashing = false;
            LastDash = Time.time;
        }
    }

    public IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        while (Time.time < startTime + DashDuration)
        {
            characterController.SimpleMove(DashSpeed * transform.forward);
            yield return null;
        }
    }

    private float DeltaCurrentTime(float time)
    {
        return Time.time - time;
    }
}
