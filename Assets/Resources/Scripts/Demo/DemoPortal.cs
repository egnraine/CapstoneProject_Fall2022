using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;
using UnityEngine.UI;

public class DemoPortal : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    public Player player;
    public Animator anim;
    private bool dialogueCalled = false;
    public Image blur;

    public AudioSource BGM;
    public AudioSource doorOpen;

    static DemoPortal instance;

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

        BGM = GameObject.Find("AudioBGM").GetComponent<AudioSource>();
        doorOpen = GameObject.Find("AudioDoor").GetComponent<AudioSource>();
    }

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        int selectedChoice = 0;
        int currentlevel = GameManager.instance.GetCurrentLevel() - 1;

        if (collision.name == "Player")
        {
            if (Input.GetMouseButtonDown(1) && currentlevel == 0)
            {
                yield return DialogueManager.instance.ShowDialogueText($"A door.");
            }

            else if (Input.GetMouseButtonDown(1) && dialogueCalled == false && currentlevel > 0)
            {
                yield return DialogueManager.instance.ShowDialogue(dialogue, new List<string>() { "Yes", "No" }, 
                    (choiceIndex) => selectedChoice = choiceIndex);

                if (selectedChoice == 0)
                {
                    // Yes

                    dialogueCalled = true;

                    BGM.pitch = 0.75f;
                    anim.speed = 1.3f;

                    CameraShaker.Instance.StartShake(.5f, .5f, 0.1f);
                    blur.enabled = true;

                    yield return DialogueManager.instance.ShowDialogueText($"...I'm not ready yet. My heart is racing...");
                }

                else if (selectedChoice == 1)
                {
                    // No

                    yield return DialogueManager.instance.ShowDialogueText($"I don't think I can do it...");
                }
            }

            else if (dialogueCalled == true && anim.speed == 0.4f && Input.GetMouseButtonDown(1))
            {
                // Teleport Player
                anim.speed = 0.4f;
                doorOpen.Play();

                SceneManager.LoadScene("Farm");

                GameManager.instance.GrantXp(5);
            }
        }
    }
}
