using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    //Components
    private CharacterMovementComponent characterMovementComponent;
    private InteractorComponent interactorComponent;
    private PlayerInventoryComponent playerInventoryComponent;
    public DeathScreenGUI deathScreenGUI;

    //temp, need to fix HitPoints to be private?
    [SerializeField] float MaxHitPoints = 100f;
    public float HitPoints = 0f;

    //Movement relative to camera
    public Transform cameraTransform;

    public void Reset()
    {
        this.AddComponent<CharacterMovementComponent>();
        this.AddComponent<InteractorComponent>();
        this.AddComponent<PlayerInventoryComponent>();
    }
    public void Start()
    {
        characterMovementComponent = this.GetComponent<CharacterMovementComponent>();
        interactorComponent = this.GetComponent<InteractorComponent>();
        playerInventoryComponent = this.GetComponent<PlayerInventoryComponent>();
        HitPoints = MaxHitPoints;
    }
    public void Update()
    {
        bool primaryAction = Input.GetButtonDown("PrimaryAction");
        bool interact = Input.GetButtonDown("Interact");

        //Temp buttonchecks?
        bool dropItem = Input.GetKeyDown("g");
        bool numberKey1 = Input.GetKeyDown("1");
        bool numberKey2 = Input.GetKeyDown("2");
        bool numberKey3 = Input.GetKeyDown("3");
        bool numberKey4 = Input.GetKeyDown("4");
        bool numberKey5 = Input.GetKeyDown("5");
        bool numberKey6 = Input.GetKeyDown("6");

        if (primaryAction)
        {
            playerInventoryComponent.UseActiveItemPrimaryAction();
        }

        if (interact)
        {
            interactorComponent.Interact();
        }

        if (dropItem)
        {
            playerInventoryComponent.DropItem();
        }

        if (numberKey1)
        {
            playerInventoryComponent.SelectItemByIndex(0);
        }
        if (numberKey2)
        {
            playerInventoryComponent.SelectItemByIndex(1);
        }
        if (numberKey3)
        {
            playerInventoryComponent.SelectItemByIndex(2);
        }
        if (numberKey4)
        {
            playerInventoryComponent.SelectItemByIndex(3);
        }
        if (numberKey5)
        {
            playerInventoryComponent.SelectItemByIndex(4);
        }
        if (numberKey6)
        {
            playerInventoryComponent.SelectItemByIndex(5);
        }
    }
    public void FixedUpdate()
    {
        //Eventually just smooth input yourself
        Vector2 smoothedMovement = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1.0f);
        Vector2 rawMovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Transform cameraTransform = Camera.main.transform;
        characterMovementComponent.MovePlayerRelativeToCamera(new Vector3(smoothedMovement.x, 0.0f, smoothedMovement.y), new Vector3(rawMovementInput.x, 0.0f, rawMovementInput.y), cameraTransform);
    }

    // Basic implementation for taking damage, can modify later
    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0) {
            PlayerDeath();
        }
    }
    // Basic implementation for player health
    public void UpdateHealth(float mod) {
        HitPoints += MaxHitPoints;

        if (HitPoints > MaxHitPoints) {
            HitPoints = MaxHitPoints;
        } else if (HitPoints <= 0f) {
            HitPoints = 0f;
            PlayerDeath();
        }
    }
    public void PlayerDeath()
    {
        deathScreenGUI.ShowDeathScreen();
    }
}
