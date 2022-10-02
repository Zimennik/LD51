using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWire : MonoBehaviour, IInteractable, IResetable
{
    private bool _isRepaired = true;

    public string InteractionText
    {
        get
        {
            if (CharacterController.Instance.inventory.HasItemByName("Duct Tape"))
            {
                return "Fix the wire";
            }
            else
            {
                return "The wire is broken";
            }
        }
    }

    public void Interact()
    {
        if (CharacterController.Instance.inventory.HasItemByName("Duct tape"))
        {
            StartCoroutine(Repairing());
        }
        else
        {
            UIController.Instance.DialogueSystem.ShowDialogue(new List<string>
                { "You don't have anything to fix the wire with." });
        }
    }

    IEnumerator Repairing()
    {
        CharacterController.Instance.StartCutscene();
        UIController.Instance.ShowFade(1f, Color.black);
        yield return new WaitForSeconds(0.5f);
        _isRepaired = true;
        //Set resetable flag

        CharacterController.Instance.flagController.SetFlag("phone_repaired", true);

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);

        UIController.Instance.DialogueSystem.ShowDialogue(new List<string>() { "The wire is fixed!" });
    }

    public void ResetObject()
    {
        _isRepaired = false;
        gameObject.SetActive(true);
    }
}