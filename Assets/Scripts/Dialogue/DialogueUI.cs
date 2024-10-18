using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DialogueUI
{
    // Hey! Mysterious Man over here. C# works like Java, in which you need to put everything inside a class for it to work
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;

    void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        dialogueText.text = dialogue;
        dialoguePanel.SetActive(true);
    }

    void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;;
        dialoguePanel.SetActive(false);
    }
}
