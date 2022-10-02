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
        first_dialogue.Add("Breaking news started on TV, but it broke down again.");
        first_dialogue.Add("Last time, Jake fixed it with a wrench.");

        var second_dialogue = new List<string>();
        second_dialogue.Add("Did you check the Broom Closet?");
        second_dialogue.Add("I think wrench is in there.");


        var alienDialogues = new List<string>();
        alienDialogues.Add("Oh no! Aliens!");
        alienDialogues.Add("I don't want to die!");
        alienDialogues.Add("I have 2 days until retirement!");

        if (CharacterController.Instance.flagController.GetFlag("tv_repaired"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(alienDialogues);
            return;
        }


        if (CharacterController.Instance.flagController.GetFlag("npc_tv_talked") == false)
        {
            UIController.Instance.DialogueSystem.ShowDialogue(first_dialogue);
            CharacterController.Instance.flagController.SetFlag("npc_tv_talked", true);
            CharacterController.Instance.flagController.SetFlag("player_knows_about_wrench", true);
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