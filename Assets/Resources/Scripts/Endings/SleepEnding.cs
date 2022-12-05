using System.Collections;
using UnityEngine;

public class SleepEnding : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    public Animator gameOver;

    public Animator anim;

    private void Awake()
    {
        GameObject.Find("Player").GetComponent<Player>().LockMovement();

        anim = GameObject.Find("Player").GetComponent<Animator>();
        gameOver = GameObject.Find("GameOverContainer").GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        yield return DialogueManager.instance.ShowDialogue(dialogue);

        anim.SetTrigger("sleep");

        yield return new WaitForSeconds(3.5f);
        gameOver.SetTrigger("show");
    }
}
