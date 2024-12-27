using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    // Player components
    private CharacterMovementComponent characterMovementComponent;
    private InteractorComponent interactorComponent;
    private PlayerInventoryComponent playerInventoryComponent;

    // Hitpoint values
    public float MaxHitPoints = 100f;
    public float HitPoints = 0f;

    // Event handler called on player death
    public event EventHandler<bool> OnDeath;

    // Dialogue variables
    public bool IsInConversation;

    public void Reset()
    {
        this.AddComponent<CharacterMovementComponent>();
        this.AddComponent<InteractorComponent>();
        this.AddComponent<PlayerInventoryComponent>();
    }
    public void Start()
    {
        // Retrieve various components of the player
        characterMovementComponent = GetComponent<CharacterMovementComponent>();
        interactorComponent = GetComponent<InteractorComponent>();
        playerInventoryComponent = GetComponent<PlayerInventoryComponent>();

        // Set hitpoints
        HitPoints = MaxHitPoints;
        UpdateHealth();

        // Set InConversation on dialogue events
        DialogueBoxController.OnDialogueStart += () => { IsInConversation = true;
            Debug.Log("Dialogue started");
        };
        DialogueBoxController.OnDialogueEnd += () => { IsInConversation = false;
            Debug.Log("Dialogue ended");
        };
    }
    public void Update()
    {
        // When interacting, call interact component
        bool interact = Input.GetButtonDown("Interact");

        if (interact)
        {
            interactorComponent.Interact();
        }

        // Stop other interaction when in conversation
        if (!IsInConversation)
        {
            // Select inventory item slot
            bool numberKey1 = Input.GetKeyDown("1");
            bool numberKey2 = Input.GetKeyDown("2");
            bool numberKey3 = Input.GetKeyDown("3");
            bool numberKey4 = Input.GetKeyDown("4");
            bool numberKey5 = Input.GetKeyDown("5");
            bool numberKey6 = Input.GetKeyDown("6");

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

            // Activate primary action of currently selected item
            bool primaryAction = Input.GetButtonDown("PrimaryAction");

            if (primaryAction)
            {
                playerInventoryComponent.UseActiveItemPrimaryAction();
            }

            // Drop currently selected item
            bool dropItem = Input.GetKeyDown("g");

            if (dropItem)
            {
                playerInventoryComponent.DropItem();
            }

            // Complete dash movement
            bool dash = Input.GetKeyDown("space");

            if (dash)
            {
                StartCoroutine(characterMovementComponent.PlayerDash());
            }
        }
    }

    public void FixedUpdate()
    {
        // Stop other interaction when in conversation
        if (!IsInConversation)
        {
            // Clamp magnitude of movement
            Vector2 smoothedMovement = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1.0f);
            Vector2 rawMovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            characterMovementComponent.MovePlayerRelativeToCamera(new Vector3(smoothedMovement.x, 0.0f, smoothedMovement.y), new Vector3(rawMovementInput.x, 0.0f, rawMovementInput.y), Camera.main.transform);
        }
    }

    // Basic implementation for taking damage
    public void TakeDamage(float damage)
    {
        // Take damage
        HitPoints -= damage;

        // Check HP values
        UpdateHealth();
    }


    // Basic implementation for player health
    public void UpdateHealth() {
        // Limit HP to max if exceeded
        if (HitPoints > MaxHitPoints)
        {
            HitPoints = MaxHitPoints;
        }

        // Check if player is dead
        else if (HitPoints <= 0f)
        {
            HitPoints = 0f;
            // Invoke death event
            OnDeath.Invoke(this, true);
        }
    }
}
