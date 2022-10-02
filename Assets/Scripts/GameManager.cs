using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum GameState
{
    Start,
    GameCycle,
    Ending
}

public class GameManager : MonoBehaviour
{
    public GameState CurrentState { get; private set; }

    //singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void PlayFirstCutscene()
    {
    }

    public void PlayEndingCutscene()
    {
        StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        CurrentState = GameState.Ending;
        //wait until the CharacterController.Instance.IsCutscene is false
        yield return new WaitUntil(() => CharacterController.Instance.IsCutscene == false);

        //play ending cutscene
    }


    public void StartGame()
    {
        StartCoroutine(GameCycle());
    }

    IEnumerator GameCycle()
    {
        CurrentState = GameState.GameCycle;
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