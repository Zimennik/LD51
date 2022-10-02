using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakableItem : MonoBehaviour, IInteractable, IResetable
{
    [SerializeField] private ItemSO item;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider2D _collder;

    [SerializeField] private bool isVisible = true;


    public void ShowItem()
    {
        _collder.enabled = true;
        _renderer.enabled = true;
    }

    public void HideItem()
    {
        _collder.enabled = false;
        _renderer.enabled = false;
    }

    public string InteractionText => $"Press Space to take {item.Name}";

    public void Interact()
    {
        CharacterController.Instance.inventory.AddItemByName(item.Name);
        HideItem();
        UIController.Instance.DialogueSystem.ShowDialogue(new List<string>() { $"You took {item.Name}" });
    }

    public void EnterInteractionZone()
    {
    }

    public void ExitInteractionZone()
    {
    }

    public void ResetObject()
    {
        if (isVisible)
        {
            ShowItem();
        }
        else
        {
            HideItem();
        }
    }
}