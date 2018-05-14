using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactuable : MonoBehaviour {

    [SerializeField] private Dialogue dialogue;
    private Player player;
    private int currentDialogueFrame;

    private void Start()
    {
        player = gameManager.instance.player.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.interactionTarget = this;
            currentDialogueFrame = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.interactionTarget == this) player.interactionTarget = null;
            currentDialogueFrame = 0;
        }
    }

    public void nextDialogue()
    {
        if (currentDialogueFrame == dialogue.fullDialogue.Count) endConversation();
        else
        {
            player.textBox.showDialogue(dialogue.fullDialogue[currentDialogueFrame]);
            currentDialogueFrame++;
        }
    }

    public void endConversation()
    {
        gameManager.instance.IsPaused = false;
        gameManager.instance.InScene = false;
        player.textBox.closeTextBox();
        currentDialogueFrame = 0;
    }
}
