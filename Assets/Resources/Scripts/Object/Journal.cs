using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Journal : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    public bool playerInRange;
    public GameObject characterMenu;

    bool dialogueCalled = false;

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1) && dialogueCalled == false)
            {
                yield return DialogueManager.instance.ShowDialogue(dialogue);
                GameManager.instance.GrantXp(1);

                characterMenu.SetActive(true);
                dialogueCalled = true;
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1) && dialogueCalled == true)
            {
                yield return DialogueManager.instance.ShowDialogueText($"A journal.");
            }
        }

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player out of range");
            playerInRange = false;
        }
    }
}
