using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredEnding : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    public Animator player;
    public Animator gameOver;

    public AudioSource BGM;

    private void Awake()
    {
        GameObject.Find("Player").GetComponent<Player>().LockMovement();

        player = GameObject.Find("Player").GetComponent<Animator>();
        gameOver = GameObject.Find("ScaredEndingContainer").GetComponent<Animator>();

        BGM = GameObject.Find("AudioBGM").GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
        yield return DialogueManager.instance.ShowDialogue(dialogue);
        BGM.Stop();

        player.SetTrigger("rest");

        yield return new WaitForSeconds(2f);
        gameOver.SetTrigger("show");
    }

}

