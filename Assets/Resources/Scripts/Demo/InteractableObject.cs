using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
            {
                yield return DialogueManager.instance.ShowDialogueText("I'm not interested in this.");
            }
        }
    }
}
