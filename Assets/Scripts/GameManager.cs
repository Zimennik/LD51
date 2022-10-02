using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public enum GameState
{
    Start,
    GameCycle,
    Ending
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image _introFade;
    [SerializeField] private GameObject _characterController;
    [SerializeField] private GameObject _introCharacter;
    [SerializeField] private GameObject _death;
    [SerializeField] private GameObject _ui;

    [SerializeField] private GameObject _mainOffice;
    [SerializeField] private GameObject _blackVoid;

    [SerializeField] private SpriteRenderer _alien;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private AudioClip _alienSound;


    public GameState CurrentState { get; private set; }

    //singleton
    public static GameManager Instance { get; private set; }

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


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Intro());
    }

    public void PlayFirstCutscene()
    {
    }

    public void PlayEndingCutscene()
    {
        StartCoroutine(Ending());
    }

    IEnumerator Intro()
    {
        _ui.SetActive(false);
        _death.SetActive(false);
        _introFade.color = Color.black;
        _characterController.SetActive(false);
        _introCharacter.SetActive(true);

        yield return _introFade.DOFade(0, 4f);

        yield return new WaitForSeconds(3f);

        var introDialogue = new List<string>();
        introDialogue.Add("What a beautiful day.");
        introDialogue.Add("I hope nothing bad happens today.");


        UIController.Instance.DialogueSystem.ShowDialogue(introDialogue);

        //wait until cutscene is done

        CharacterController.Instance.IsCutscene = true;
        yield return new WaitUntil(() => !CharacterController.Instance.IsCutscene);

        yield return new WaitForSeconds(2);

        UIController.Instance.ShowFade(5, Color.white);
        _audioSource.PlayOneShot(_explosion);

        yield return new WaitForSeconds(2.5f);

        _mainOffice.SetActive(false);
        _blackVoid.SetActive(true);

        yield return new WaitForSeconds(5f);


        var deathDialogue = new List<string>();
        deathDialogue.Add("Hey, Bob. I have some bad news for you.");
        deathDialogue.Add("You're dead.");
        deathDialogue.Add("But not only you. Everyone is dead.");
        deathDialogue.Add("Something terrible just happened. And entire world is gone.");
        deathDialogue.Add("...");
        deathDialogue.Add("But you can help me to fix it.");
        deathDialogue.Add("I need you to go back in time and prevent this from happening.");
        deathDialogue.Add("You have only 10 seconds to do it.");
        deathDialogue.Add("But I will send you back over and over until you succeed.");
        deathDialogue.Add("If you don't know what to do next, you can ask me for advice.");
        deathDialogue.Add("Good luck, Bob.");


        UIController.Instance.DialogueSystem.ShowDialogue(deathDialogue);
        yield return new WaitUntil(() => !CharacterController.Instance.IsCutscene);

        UIController.Instance.ShowFade(5, Color.black);

        yield return new WaitForSeconds(2.5f);

        _blackVoid.SetActive(false);
        _mainOffice.SetActive(true);
        _death.SetActive(true);


        _characterController.SetActive(true);
        _introCharacter.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        StartGame();
    }

    IEnumerator Ending()
    {
        CurrentState = GameState.Ending;
        //wait until the CharacterController.Instance.IsCutscene is false
        yield return new WaitUntil(() => CharacterController.Instance.IsCutscene == false);

        CharacterController.Instance.IsCutscene = true;

        _audioSource.Stop();

        _audioSource.PlayOneShot(_alienSound);
        _alien.DOFade(0, 5f).OnComplete((() => { _introFade.DOFade(1, 5f); }));

        yield return new WaitForSeconds(10f);

        UIController.Instance.ShowEndingScreen();

        //Show ending screen

        //play ending cutscene
    }


    public void StartGame()
    {
        StartCoroutine(GameCycle());
    }

    IEnumerator GameCycle()
    {
        _ui.SetActive(true);
        CurrentState = GameState.GameCycle;
        _audioSource.Play();
        while (CurrentState == GameState.GameCycle)
        {
            for (int i = 0; i < 100; i++)
            {
                if (CurrentState == GameState.Ending)
                {
                    yield break;
                }

                if (CharacterController.Instance.IsCutscene)
                {
                    // wait for cutscene to finish
                    yield return new WaitUntil(() => !CharacterController.Instance.IsCutscene);
                }


                yield return new WaitForSeconds(0.1f);
                UIController.Instance.TimerController.SetTime(i, 100);
            }

            if (CharacterController.Instance.IsCutscene)
            {
                // wait for cutscene to finish
                yield return new WaitUntil(() => !CharacterController.Instance.IsCutscene);
            }


            CharacterController.Instance.IsCutscene = true;
            //play reset cutscene
            UIController.Instance.ShowFade(2f, Color.white);
            UIController.Instance.TimerController.SetTime(100, 100);
            yield return new WaitForSeconds(1f);
            ResetWorld();
            UIController.Instance.TimerController.SetTime(0, 100);
            yield return new WaitForSeconds(2f);
            CharacterController.Instance.IsCutscene = false;
        }
    }


    public void ResetWorld()
    {
        //find all IResetable objects and reset them
        //even if they are disabled

        var resetables = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)).OfType<IResetable>();
        foreach (var resetable in resetables)
        {
            resetable.ResetObject();
        }
    }
}