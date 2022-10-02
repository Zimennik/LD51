using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Death : MonoBehaviour, IInteractable
{
    public string InteractionText => "Talk to Grim Reaper";

    public void Interact()
    {
        // First dialogue
        var dialogue1 = new List<string>();
        dialogue1.Add("You don't have much time left, do you?");
        dialogue1.Add("The first thing you need to figure out is what's going on.");
        dialogue1.Add("Try asking around, maybe someone will know something.");

        //After player knows about the aliens
        var dialogue2 = new List<string>();
        dialogue2.Add("So, Aliens, huh?");
        dialogue2.Add("I don't know much about them, but I do know that they're not friendly.");
        dialogue2.Add("They're here to take over the world, and they're not going to stop until they do.");
        dialogue2.Add("But maybe you can convince them.");
        dialogue2.Add("I heard some alien-like noises coming from the roof");
        dialogue2.Add("You should check it out.");

        //After player knows about alien on a roof
        var dialogue3 = new List<string>();
        dialogue3.Add("So, you found the alien on the roof?");
        dialogue3.Add("I don't know what you're going to do with it, but maybe NASA can help you.");
        dialogue3.Add("Try to get their number somehow.");

        //After player knows about broken phone
        var dialogue4 = new List<string>();
        dialogue4.Add("So, office phone is broken?");
        dialogue4.Add("Most likely the wire is damaged.");
        dialogue4.Add("You'll need find something to fix it.");

        //After player knows alien language
        var dialogue5 = new List<string>();
        dialogue5.Add("So, you know alien language?");
        dialogue5.Add("That's great!");
        dialogue5.Add("Now you can talk to the alien on the roof.");
        dialogue5.Add("Good luck!");


        if (CharacterController.Instance.flagController.GetFlag("player_knows_alien_language"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue5);
            return;
        }

        if (CharacterController.Instance.flagController.GetFlag("player_knows_about_broken_phone"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue4);
            return;
        }

        if (CharacterController.Instance.flagController.GetFlag("player_knows_about_alien_on_roof"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue3);
            return;
        }

        if (CharacterController.Instance.flagController.GetFlag("player_knows_about_invasion"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue2);
            return;
        }

        UIController.Instance.DialogueSystem.ShowDialogue(dialogue1);
    }
}