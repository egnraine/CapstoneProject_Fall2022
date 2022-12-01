using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] ChoiceBox choiceBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] int lettersPerSecond;


    public static DialogueManager instance { get; private set; }

    private void Awake()
    {
        if (DialogueManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public bool isShowing { get; private set; }

    public IEnumerator ShowDialogueText(string text, bool waitForInput = true, bool autoClose = true)
    {
        isShowing = true;
        dialogueBox.SetActive(true);

        yield return WriteText(text);

        if (waitForInput)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (autoClose)
        {
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        isShowing = false;

        GameObject.Find("Player").GetComponent<Player>().UnlockMovement();
    }

    public IEnumerator ShowDialogue(Dialog dialogue, List<string> choices = null, Action<int> onChoiceSelected = null)
    {
        yield return new WaitForEndOfFrame();

        isShowing = true;
        dialogueBox.SetActive(true);
        
        foreach (var line in dialogue.Lines)
        {
            yield return WriteText(line);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (choices  != null && choices.Count > 1)
        {
            yield return choiceBox.ShowChoices(choices, onChoiceSelected);
        }

        dialogueBox.SetActive(false);
        isShowing = false;

        GameObject.Find("Player").GetComponent<Player>().UnlockMovement();
    }

    public IEnumerator WriteText(string line)
    {
        GameObject.Find("Player").GetComponent<Player>().LockMovement();

        dialogueText.text = "";

        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
}
