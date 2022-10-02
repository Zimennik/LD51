using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _interactText;
    [SerializeField] private CanvasGroup _interactableTextHolder;
    [SerializeField] public DialogueSystem DialogueSystem;
    [SerializeField] public TimerController TimerController;

    [SerializeField] public CanvasGroup _whiteFade;
    [SerializeField] private Image _fadeImage;

    [SerializeField] private CanvasGroup _endingScreen;


    private bool _isEnding = false;

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

    private void Update()
    {
        if (_isEnding)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }

    public void ShowEndingScreen()
    {
        _endingScreen.DOFade(1, 1).OnComplete(() => _isEnding = true);
    }


    public void ShowInteractionText(string Text)
    {
        _interactText.text = Text;
        _interactableTextHolder.DOFade(1, 0.1f);
    }

    public void HideInteractionText()
    {
        _interactableTextHolder.DOFade(0, 0.1f);
    }

    public void ShowFade(float time, Color color)
    {
        _whiteFade.alpha = 0;
        StartCoroutine(ShowingFade(time, color));
    }

    private IEnumerator ShowingFade(float time, Color color)
    {
        _fadeImage.color = color;
        _whiteFade.DOFade(1, time / 2);
        yield return new WaitForSeconds(time);
        _whiteFade.DOFade(0, time / 2);
    }
}