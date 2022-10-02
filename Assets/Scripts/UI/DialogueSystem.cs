using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private CanvasGroup _dialogueHolder;

    [SerializeField] private AudioClip _talkSound;
    [SerializeField] private AudioSource _audioSource;

    private bool _nextSentence = false;

    public void ShowDialogue(List<string> dialogue)
    {
        _nextSentence = false;
        _dialogueHolder.DOFade(1, 0.1f);
        StartCoroutine(ShowDialogueCoroutine(dialogue));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _nextSentence = true;
        }
    }

    private IEnumerator ShowDialogueCoroutine(List<string> dialogue)
    {
        CharacterController.Instance.StartCutscene();
        foreach (var sentence in dialogue)
        {
            //if sentence contains "[add_item(%item_name%)]", then add item to inventory by name and remove this part of string
            //if sentence contains "[flag(%flag_name%), %value%]", then set flag by name and remove this part of string

            Regex regex = new Regex(@"\[add_item\((\w+)\)\]");
            Match match = regex.Match(sentence);
            if (match.Success)
            {
                string itemName = match.Groups[1].Value;
                CharacterController.Instance.inventory.AddItemByName(itemName);
                sentence.Replace(match.Value, "");
            }

            regex = new Regex(@"\[flag\((\w+)\), (\w+)\]");
            match = regex.Match(sentence);
            if (match.Success)
            {
                string flagName = match.Groups[1].Value;
                bool flagValue = (match.Groups[2].Value == "true" || match.Groups[2].Value == "1");
                CharacterController.Instance.flagController.SetFlag(flagName, flagValue);
                sentence.Replace(match.Value, "");
            }


            _dialogueText.maxVisibleCharacters = 0;
            _dialogueText.text = sentence;

            if (!_nextSentence)
            {
                //show character by character
                foreach (var letter in sentence)
                {
                    if (_nextSentence)
                    {
                        _dialogueText.maxVisibleCharacters = sentence.Length;
                        _nextSentence = false;
                        break;
                    }

                    _dialogueText.maxVisibleCharacters++;
                    _audioSource.PlayOneShot(_talkSound);
                    yield return new WaitForSeconds(0.05f);
                }
            }


            yield return new WaitUntil(() => _nextSentence);
            _nextSentence = false;
        }

        _dialogueHolder.DOFade(0, 0.1f);
        CharacterController.Instance.EndCutscene();
    }
}