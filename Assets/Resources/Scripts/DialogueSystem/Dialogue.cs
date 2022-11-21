using System.Collections;
using UnityEngine;
using TMPro;

namespace DialogueSystem
{
    public class Dialogue : DialogueBase
    {
        private TextMeshProUGUI textHolder;

        [Header("Text")]
        [SerializeField] private TMP_FontAsset textfont;
        [SerializeField] private string input;

        [Header("Time Parameters")]
        [SerializeField] private float delay;
        [SerializeField] private float delayLines;

        private IEnumerator lineAppear;

        private void Awake()
        {
            textHolder = GetComponent<TextMeshProUGUI>();
            textHolder.text = "";
        }

        private void OnEnable()
        {
            ResetLine();
            lineAppear = (WriteText(input, textHolder, textfont, delay, delayLines));
            StartCoroutine(lineAppear);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }

                else
                    finished = true;
                    GameObject.Find("Player").GetComponent<Player>().UnlockMovement();
            }
        }

        private void ResetLine()
        {
            textHolder = GetComponent<TextMeshProUGUI>();
            textHolder.text = "";
            finished = false;
        }
    }
}
