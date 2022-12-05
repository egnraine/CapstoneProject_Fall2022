using System.Collections;
using UnityEngine;

public class GoodCutscene : MonoBehaviour
{
    [SerializeField] Dialog dialogue;

    public Animator gameOver;

    public Animator playerAnim;

    private void Awake()
    {
        GameObject.Find("Player").GetComponent<Player>().LockMovement();

        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        gameOver = GameObject.Find("GoodCutsceneContainer").GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        playerAnim.SetTrigger("breathe");

        yield return new WaitForSeconds(1f);
        yield return DialogueManager.instance.ShowDialogue(dialogue);

        yield return new WaitForSeconds(2.5f);
        gameOver.SetTrigger("show");
    }
}
