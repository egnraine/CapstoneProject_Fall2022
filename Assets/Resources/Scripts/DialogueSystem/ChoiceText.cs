using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceText : MonoBehaviour
{
    TextMeshProUGUI text;
    Color highlightedColor = Color.cyan;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetSelected(bool selected)
    {
        text.color = (selected)? highlightedColor : Color.black;
    }

    public TextMeshProUGUI TextField => text;
}
