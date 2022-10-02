using System.Collections.Generic;
using UnityEngine;

public class RoofNPC : MonoBehaviour, IInteractable
{
    public string InteractionText => "Talk";

    public void Interact()
    {
        List<string> dialogue = new List<string>();

        dialogue.Add("Hey!");
        dialogue.Add("I heard some strange noises coming from the roof.");
        dialogue.Add("But I can't check it out because I'm too scared.");
        dialogue.Add("Also, this door only opens during a fire alarm.");


        var fire_dialogue = new List<string>();

        fire_dialogue.Add("Did you just set off the fire alarm?");
        fire_dialogue.Add("Well, I hope this is worth it.");

        if (CharacterController.Instance.flagController.GetFlag("fire_alarm"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(fire_dialogue);
        }
        else
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue);
        }
    }

    public void EnterInteractionZone()
    {
    }

    public void ExitInteractionZone()
    {
        throw new System.NotImplementedException();
    }
}