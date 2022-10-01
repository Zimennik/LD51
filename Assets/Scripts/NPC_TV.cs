using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_TV : MonoBehaviour, IInteractable
{
    public string InteractionText => "Press Space to talk";

    public void Interact()
    {
        Debug.Log("Interacted");


        //test dialogue
        var first_dialogue = new List<string>();
        first_dialogue.Add("Hello");
        first_dialogue.Add("How are you?");
        first_dialogue.Add("I'm fine, thanks");
        first_dialogue.Add("Goodbye");

        var second_dialogue = new List<string>();
        second_dialogue.Add("We already talked");
        second_dialogue.Add("Goodbye");


        if (CharacterController.Instance.flagController.GetFlag("npc_tv_talked") == false)
        {
            UIController.Instance.DialogueSystem.ShowDialogue(first_dialogue);
            CharacterController.Instance.flagController.SetFlag("npc_tv_talked", true);
        }
        else
        {
            UIController.Instance.DialogueSystem.ShowDialogue(second_dialogue);
        }
    }

    public void EnterInteractionZone()
    {
        Debug.Log("Entered");
    }

    public void ExitInteractionZone()
    {
        Debug.Log("Exited");
    }
}