using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedClass : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    public string[] sceneNames;
    public int counter;

    public GameObject globalVol;
    static BedClass instance;

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

        globalVol = GameObject.Find("GlobalVolume");
    }

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        int selectedChoice = 0;

        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
            {
                yield return DialogueManager.instance.ShowDialogue(dialogue, new List<string>() { "Yes", "No" },
                    (choiceIndex) => selectedChoice = choiceIndex);

                if (selectedChoice == 0)
                {
                    // Yes

                    ResetDay();
                }

                else if (selectedChoice == 1)
                {
                    // No

                    DialogueManager.instance.CloseDialogue();
                }

                if (counter == 3 && GameManager.instance.GetCurrentLevel() - 1 == 0)
                {
                    BadEnding();
                }

                if (counter == 5 && GameManager.instance.GetCurrentLevel() - 1 == 1)
                {
                    BadEnding();
                }
            }
        }
    }

    public void ResetDay()
    {
        SceneManager.LoadScene("Home");

        counter++;
        Debug.Log("Counter: " + counter);

        globalVol.GetComponent<DayNightCycle>().hours = 7;
        globalVol.GetComponent<DayNightCycle>().mins = 0;
    }

    public void BadEnding()
    {
        string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
        SceneManager.LoadScene(sceneName);
    }
}
