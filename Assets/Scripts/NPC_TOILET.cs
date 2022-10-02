using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_TOILET : MonoBehaviour, IInteractable, IResetable
{
    private bool paperGiven = false;

    public string InteractionText => "Talk";

    public void Interact()
    {
        var dialogue1 = new List<string>();
        dialogue1.Add("Hey dude!");
        dialogue1.Add("I REALLY in a bad situation here!");
        dialogue1.Add("Can you find me a toilet paper?");


        //player has to find toilet paper

        var dialogue2 = new List<string>();
        dialogue2.Add("Thank you so much!");
        dialogue2.Add("Here is duct tape for you!");
        dialogue2.Add("* You got a duct tape *");

        var dialogue3 = new List<string>();
        dialogue3.Add("Thanks again! You saved me!");

        if (paperGiven)
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue3);
            return;
        }
        else
        {
            if (CharacterController.Instance.inventory.HasItemByName("Toilet paper"))
            {
                UIController.Instance.DialogueSystem.ShowDialogue(dialogue2);
                CharacterController.Instance.inventory.AddItemByName("Duct tape");
                CharacterController.Instance.inventory.RemoveItemByName("Toilet paper");
            }
            else
            {
                UIController.Instance.DialogueSystem.ShowDialogue(dialogue1);
            }
        }
    }

    public void ResetObject()
    {
        paperGiven = false;
    }
}