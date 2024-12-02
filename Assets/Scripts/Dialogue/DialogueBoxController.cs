using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueBoxController : MonoBehaviour
{
    private static DialogueBoxController instance;

    [SerializeField] GameObject dialogueBox;
    /*TextMeshProUGUI nameText;
    TextMeshProUGUI dialogueText;*/

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;

    public static DialogueBoxController GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void StartDialogue(DialogueAsset dialogueAsset, int startPosition)
    {
        dialogueBox.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogueAsset.dialogue, startPosition));
    }

    /*public void StartDialogue(string[] dialogue, int startPosition, string name)
    {
        nameText.text = name + "...";
        dialogueBox.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }*/

    IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();
        dialogueBox.SetActive(true);

        for(int i = startPosition; i < dialogue.Length; i++)
        {
            string[] dialogueLine = dialogue[i].Split(':');
            var nameText = GameObject.FindWithTag("Name").GetComponent<TextMeshProUGUI>();
            var dialogueText = GameObject.FindWithTag("Dialogue").GetComponent<TextMeshProUGUI>();
            nameText.text = dialogueLine[0];
            dialogueText.text = dialogueLine[1];
            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialogueBox.SetActive(false);
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
}