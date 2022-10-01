using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogue : MonoBehaviour, IInteractable
{
    [SerializeField] private List<string> _dialogue;
    [SerializeField] private string _interactionText;

    public string InteractionText => _interactionText;

    public void Interact()
    {
        UIController.Instance.DialogueSystem.ShowDialogue(_dialogue);
    }

    public void EnterInteractionZone()
    {
    }

    public void ExitInteractionZone()
    {
    }
}