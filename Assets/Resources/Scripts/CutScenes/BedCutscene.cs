using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedCutscene : MonoBehaviour
{
    [SerializeField] Dialog dialogue;
    [SerializeField] Dialog dialogue2;
    [SerializeField] Dialog dialogue3;

    public GameObject screen;

    private bool dialogueCalled;
    static BedCutscene instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        GameObject.Find("Player").GetComponent<Player>().LockMovement();
        int selectedChoice = 0;

        if (dialogueCalled == false)
        {
            yield return DialogueManager.instance.ShowDialogue(dialogue, new List<string>() { "Yes", "No" },
                        (choiceIndex) => selectedChoice = choiceIndex);


            if (selectedChoice == 0)
            {
                // Yes

                yield return new WaitForSeconds(1f);

                yield return DialogueManager.instance.ShowDialogueText($"...");
                yield return DialogueManager.instance.ShowDialogueText($"...Maybe, I should...");

                dialogueCalled = true;

                yield return new WaitForSeconds(3f);
                screen.SetActive(false);
                this.enabled = false;
            }

            else if (selectedChoice == 1)
            {
                // No

                yield return new WaitForSeconds(1f);

                yield return DialogueManager.instance.ShowDialogueText($"...Yeah, I think I'll stay in bed longer...");

                yield return new WaitForSeconds(4f);

                yield return DialogueManager.instance.ShowDialogue(dialogue2, new List<string>() { "Yes", "No" },
                    (choiceIndex) => selectedChoice = choiceIndex);


                if (selectedChoice == 0)
                {
                    // Yes - second time

                    yield return new WaitForSeconds(1f);

                    yield return DialogueManager.instance.ShowDialogueText($"...");
                    yield return DialogueManager.instance.ShowDialogueText($"...Maybe, I should...");

                    dialogueCalled = true;

                    yield return new WaitForSeconds(3f);
                    screen.SetActive(false);
                    this.enabled = false;

                }

                else if (selectedChoice == 1)
                {
                    // No - second time

                    yield return new WaitForSeconds(1f);

                    yield return DialogueManager.instance.ShowDialogueText($"...Yeah, I've already done enough. I should just rest some more...");

                    yield return new WaitForSeconds(4f);

                    yield return DialogueManager.instance.ShowDialogue(dialogue3, new List<string>() { "Yes", "No" },
                        (choiceIndex) => selectedChoice = choiceIndex);


                    if (selectedChoice == 0)
                    {
                        // Yes - third time

                        yield return new WaitForSeconds(1f);

                        yield return DialogueManager.instance.ShowDialogueText($"...");
                        yield return DialogueManager.instance.ShowDialogueText($"...Yeah, just for a bit...");

                        dialogueCalled = true;

                        yield return new WaitForSeconds(3f);
                        screen.SetActive(false);
                        this.enabled = false;

                    }

                    else if (selectedChoice == 1)
                    {
                        // No - third time = trigger bad ending

                        yield return new WaitForSeconds(1f);

                        yield return DialogueManager.instance.ShowDialogueText($"...");
                        yield return DialogueManager.instance.ShowDialogueText($"...I don't care anymore. I feel like no one understands me...");
                        yield return DialogueManager.instance.ShowDialogueText($"I just want to be left alone...");

                        yield return new WaitForSeconds(4f);

                        screen.SetActive(false);
                        this.enabled = false;

                        SceneManager.LoadScene("SleepEnding");
                    }
                }
            }
        }

        else if (dialogueCalled == true)
        {
            this.enabled = false;
        }
    }
}
