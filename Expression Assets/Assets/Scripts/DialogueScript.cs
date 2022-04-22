using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public Text dialogueBox;
    // Update is called once per frame

    public float dialogueTime;
    float dialogueTimer;

    private void Awake()
    {
        dialogueBox.text = "Equip the Radio";
    }

    public void ChangeDialogue(string desiredDialogue)
    {
        dialogueTimer = dialogueTime;

        dialogueBox.text = desiredDialogue;
    }
}
