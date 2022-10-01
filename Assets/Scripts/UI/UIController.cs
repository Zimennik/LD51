using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactText;
    [SerializeField] private CanvasGroup _interactableTextHolder;
    [SerializeField] public DialogueSystem DialogueSystem;

    //Singleton
    public static UIController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void ShowInteractionText(string Text)
    {
        _interactText.text = Text;
        _interactableTextHolder.DOFade(1, 0.1f);
    }

    public void HideInteractionText()
    {
        _interactableTextHolder.DOFade(0,0.1f);
    }
}