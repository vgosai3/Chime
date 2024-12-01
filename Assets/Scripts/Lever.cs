using UnityEngine;

public class Lever : MonoBehaviour
{
    public enum LeverType { Rotation, Fire }
    public LeverType leverType;

    private bool isInteracting = false;
    private GameObject player;

    private JesterTurret turret;

    private void Start()
    {
        turret = GetComponentInParent<JesterTurret>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
            {
                isInteracting = true;
                player = other.gameObject;
                player.GetComponent<CharacterMovementComponent>().EnableMovement(false);
                Debug.Log("Started interacting with lever: " + leverType);
            }
        }
    }

    private void Update()
    {
        if (isInteracting)
        {
            Debug.Log("Interacting with lever: " + leverType);

            if (leverType == LeverType.Rotation)
            {
                float horizontal = Input.GetAxis("Horizontal");
                if (horizontal != 0)
                {
                    turret.RotateTurret(horizontal);
                    Debug.Log("Rotating turret with input: " + horizontal);
                }
            }
            else if (leverType == LeverType.Fire)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    turret.FireBomb();
                    Debug.Log("Fired bomb from turret.");
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                isInteracting = false;
                player.GetComponent<CharacterMovementComponent>().EnableMovement(true);
                Debug.Log("Stopped interacting with lever: " + leverType);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInteracting)
        {
            isInteracting = false;
            player.GetComponent<CharacterMovementComponent>().EnableMovement(true);
            Debug.Log("Player exited lever trigger: " + leverType);
        }
    }
}
