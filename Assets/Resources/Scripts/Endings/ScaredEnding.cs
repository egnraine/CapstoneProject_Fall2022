using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredEnding : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    public Animator player;
    public Animator gameOver;

    private void Awake()
    {
        GameObject.Find("Player").GetComponent<Player>().LockMovement();

        player = GameObject.Find("Player").GetComponent<Animator>();
        gameOver = GameObject.Find("GameOverContainer").GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        yield return DialogueManager.instance.ShowDialogue(dialogue);
        player.SetTrigger("rest");

        yield return new WaitForSeconds(2f);
        gameOver.SetTrigger("show");
    }

}

