using System;
using TMPro;
using UnityEngine;
using System.Collections;

/// <summary>
/// Class designed to facilitate dialogue box usage.
/// </summary>
public class DialogueBoxController : MonoBehaviour
{

    /// <summary>
    /// Event activated on dialogue start.
    /// </summary>
    public static event Action OnDialogueStart;
    /// <summary>
    /// Event activated on dialogue end.
    /// </summary>
    public static event Action OnDialogueEnd;
    /// <summary>
    /// Boolean set on skip line trigger.
    /// </summary>
    bool skipLineTriggered;

    // Text meshes for name and dialogue boxes
    private TextMeshProUGUI NameTM;
    private TextMeshProUGUI DialogueTM;

    private void Start()
    {
        // Get text meshes from gameobject
        NameTM = GameObject.FindWithTag("Name").GetComponent<TextMeshProUGUI>();
        DialogueTM = GameObject.FindWithTag("Dialogue").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Set name textbox.
    /// </summary>
    /// <param name="name">Name to set.</param>
    private void SetName(string name)
    {
        NameTM.text = name;
    }

    /// <summary>
    /// Set dialogue textbox.
    /// </summary>
    /// <param name="dialogue">Dialogue to set.</param>
    private void SetDialogue(string dialogue)
    {
        DialogueTM.text = dialogue;
    }

    /// <summary>
    /// Start dialogue coroutine with provided dialogue asset and start position.
    /// </summary>
    /// <param name="dialogueAsset">The dialogue asset to use.</param>
    /// <param name="startPosition">The start position of the dialogue coroutine.</param>
    public void StartDialogue(DialogueAsset dialogueAsset, int startPosition)
    {
        StopAllCoroutines();
        // Start dialogue coroutine
        StartCoroutine(RunDialogue(dialogueAsset.dialogue, startPosition));
    }

    /// <summary>
    /// Coroutine to facilitate dialogue interactions.
    /// </summary>
    /// <param name="dialogue">String array of dialogue lines.</param>
    /// <param name="startPosition">Start position of dialogue.</param>
    /// <returns></returns>
    private IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {
        // Reset skip line trigger
        skipLineTriggered = false;

        // Invoke dialogue start event
        OnDialogueStart?.Invoke();

        for(int i = startPosition; i < dialogue.Length; i++)
        {
            // Parse current dialogue line into name and dialogue
            string[] dialogueLine = dialogue[i].Split(':');

            // Set name and dialogue boxes
            SetName(dialogueLine[0]);
            SetDialogue(dialogueLine[1]);

            // Wait for the current line to be skipped
            while (skipLineTriggered == false)
            {
                yield return null;
            }
            // Reset skip line trigger
            skipLineTriggered = false;
        }
        // Invoke dialogue end event
        OnDialogueEnd?.Invoke();
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
}