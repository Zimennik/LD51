using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Alien : MonoBehaviour, IInteractable
{
    public string InteractionText => "Talk to Alien";

    public void Interact()
    {
        //dialogue 1, when player don't know alien language
        var dialogue1 = new List<string>();
        dialogue1.Add(":(;%?;?;№*?:;)($@#$@ ^T@#^@^^ ^@:??(");
        dialogue1.Add("*&^*&^ *#(*^& *%*!(*^&$ %$@%#$3$");
        dialogue1.Add("* You don't understand what he is saying * ");

        //dialogue 2, when player know alien language
        var dialogue2 = new List<string>();
        dialogue2.Add("Hell-o, humaN");
        dialogue2.Add("* You asked alien why they want to destroy the Earth *");
        dialogue2.Add("WE are tIred of your wars a-nd pollution");
        dialogue2.Add("We are tirEd of your greed and sel-fishness");
        dialogue2.Add("we Are tired of yO-ur stupidity");
        dialogue2.Add("* You tell alien that this world is not perfect, but it is the best we have *");
        dialogue2.Add("* Alien looks at you with a strange look *");
        dialogue2.Add("...");
        dialogue2.Add("We'll gi-ve you a lIttle more time to fix every_thing.");
        dialogue2.Add("SeE you in 100 y-ears");

        if (CharacterController.Instance.flagController.GetFlag("player_knows_alien_language"))
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue2);
            GameManager.Instance.PlayEndingCutscene();
        }
        else
        {
            UIController.Instance.DialogueSystem.ShowDialogue(dialogue1);
            CharacterController.Instance.flagController.SetFlag("player_knows_about_alien_on_roof", true);
        }
    }
}