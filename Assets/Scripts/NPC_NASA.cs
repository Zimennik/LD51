using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_NASA : MonoBehaviour, IInteractable
{
    public string InteractionText
    {
        get
        {
            if (CharacterController.Instance.flagController.GetFlag("phone_repaired"))
            {
                if (CharacterController.Instance.flagController.GetFlag("player_knows_NASA_number"))
                {
                    return "Call NASA";
                }
                else
                {
                    return "You don't know anyone to call";
                }
            }
            else
            {
                return "The phone is broken";
            }
        }
    }

    public void Interact()
    {
        if (CharacterController.Instance.flagController.GetFlag("phone_repaired"))
        {
            if (CharacterController.Instance.flagController.GetFlag("player_knows_NASA_number"))
            {
                // NASA dialogue


                // Player knows about the alien on the roof
                var dialogue1 = new List<string>();
                dialogue1.Add("Hello, this is NASA. How can we help you?");
                dialogue1.Add("* You tell a NASA employee that there is an alien on the roof. *");
                dialogue1.Add("HOLLY COW!");
                dialogue1.Add("You need to talk to them ASAP!");
                dialogue1.Add("* NASA employee explains how to communicate with aliens *");

                var dialogue2 = new List<string>();
                dialogue2.Add("Hello, this is NASA. How can we help you?");
                dialogue2.Add("* You asked a NASA employee what to do *");
                dialogue2.Add("I don't know. Did you saw any aliens?");
                dialogue2.Add("* You say no *");
                dialogue2.Add("Then don't waste my time. I'm busy.");

                if (CharacterController.Instance.flagController.GetFlag("player_knows_about_alien_on_roof"))
                {
                    UIController.Instance.DialogueSystem.ShowDialogue(dialogue1);
                    CharacterController.Instance.flagController.SetFlag("player_knows_alien_language", true);
                }
                else
                {
                    UIController.Instance.DialogueSystem.ShowDialogue(dialogue2);
                }
            }
        }
    }
}