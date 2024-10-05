using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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