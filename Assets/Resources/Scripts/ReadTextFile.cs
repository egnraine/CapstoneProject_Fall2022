using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ReadTextFile : MonoBehaviour
{
    [SerializeField]
    private TextAsset textAssetEntries;

    [SerializeField]
    private string[] entries;

    [SerializeField]
    private TMP_Text[] textsToPage;

    public void OnClick()
    {
        readTextAsset();
        shuffleEntries();
        applyTextToObject();
    }

    void readTextAsset()
    {
        entries = textAssetEntries.text.Split(new String[] { "|" }, StringSplitOptions.None);
    }

    void shuffleEntries()
    {
        for (int i = 0; i < 10; i++)
        {
            int index1 = UnityEngine.Random.Range(0, entries.Length);
            int index2 = index1;

            while (index1 == index2)
            {
                index2 = UnityEngine.Random.Range(0, entries.Length);
            }

            string swapString = entries[index1];
            entries[index1] = entries[index2];
            entries[index2] = swapString;

        }
    }

    void applyTextToObject()
    {
        for (int i = 0; i < textsToPage.Length; i++)
        {
            textsToPage[i].text = entries[i];
        }
    }
}
