using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeTV : MonoBehaviour, IInteractable, IResetable
{
    [SerializeField] private GameObject _brokenTV;
    [SerializeField] private GameObject _repairedTV;

    private bool _isRepaired = false;
    [SerializeField] private List<string> _tvDialogue;


    private void Awake()
    {
        ResetObject();
    }

    public string InteractionText
    {
        get
        {
            if (_isRepaired)
            {
                return "Watch TV";
            }

            if (!_isRepaired)
            {
                if (CharacterController.Instance.inventory.HasItemByName("Wrench"))
                {
                    return "Repair TV";
                }
                else
                {
                    return "TV is broken";
                }
            }

            return "What?";
        }
    }


    public void Interact()
    {
        if (_isRepaired)
        {
            UIController.Instance.DialogueSystem.ShowDialogue(_tvDialogue);
            //set flag
            CharacterController.Instance.flagController.SetFlag("player_knows_about_invasion", true);
        }
        else
        {
            if (CharacterController.Instance.inventory.HasItemByName("Wrench"))
            {
                StartCoroutine(Repairing());
            }
            else
            {
                UIController.Instance.DialogueSystem.ShowDialogue(new List<string>()
                {
                    $"TV is broken. {(CharacterController.Instance.flagController.GetFlag("player_knows_about_wrench") ? "Maybe if you hit it hard enough, it might help. You should check broom closet." : "")}"
                });
            }
        }
    }

    IEnumerator Repairing()
    {
        CharacterController.Instance.StartCutscene();
        UIController.Instance.ShowFade(1f, Color.black);
        yield return new WaitForSeconds(0.5f);
        _isRepaired = true;
        _brokenTV.SetActive(false);
        _repairedTV.SetActive(true);
        //Set resetable flag

        CharacterController.Instance.flagController.SetFlag("tv_repaired", true);

        yield return new WaitForSeconds(0.5f);

        UIController.Instance.DialogueSystem.ShowDialogue(new List<string>() { "The TV is fixed!" });
    }

    public void EnterInteractionZone()
    {
    }

    public void ExitInteractionZone()
    {
    }

    public void ResetObject()
    {
        _isRepaired = false;
        _brokenTV.SetActive(true);
        _repairedTV.SetActive(false);
    }
}