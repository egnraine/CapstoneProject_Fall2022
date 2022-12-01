using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoDialogue : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    private IEnumerator Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;

        if (buildIndex == 2) // if build index is equal to "Home"
        {
            yield return DialogueManager.instance.ShowDialogue(dialogue);
        }
    }
}
