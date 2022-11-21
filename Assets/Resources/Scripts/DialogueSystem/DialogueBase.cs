using System.Collections;
using UnityEngine;
using TMPro;


namespace DialogueSystem
{
    public class DialogueBase : MonoBehaviour
    {
        public bool finished { get; protected set; }

        protected IEnumerator WriteText(string input, TextMeshProUGUI textholder, TMP_FontAsset textFont, float delay, float delayLines)
        {
            textholder.font = textFont;
            GameObject.Find("Player").GetComponent<Player>().LockMovement();


            for (int i = 0; i < input.Length; i++)
            {
                textholder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            finished = true;
            GameObject.Find("Player").GetComponent<Player>().UnlockMovement();
        }
    }
}
