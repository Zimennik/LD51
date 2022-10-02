using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MeetingRoom : MonoBehaviour, IInteractable
{
    public string InteractionText => "Talk";

    public void Interact()
    {
        List<string> dialogue = new List<string>();

        dialogue.Add("Someone broke the phone.");
        dialogue.Add("Michael has duct tape, but he hasn't left the toilet for an hour.");
        dialogue.Add("Can you check is he's okay?");

        //telephone is repaired

        List<string> dialogue2 = new List<string>();
        dialogue2.Add("Thanks for fixing the phone.");

        List<string> dialogue3 = new List<string>();
        dialogue3.Add("Oh, you need NASA phone number?");
        dialogue3.Add("I used to work for NASA");
        dialogue3.Add("Here is the number:");
        dialogue3.Add("*Now you know NASA phone number*");


        //


        if (CharacterController.Instance.flagController.GetFlag("phone_repaired"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue3);
            CharacterController.Instance.flagController.SetFlag("player_knows_NASA_number", true);
            return;
        }
        else
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue);
            CharacterController.Instance.flagController.SetFlag("player_knows_about_broken_phone", true);
            return;
        }
    }
}