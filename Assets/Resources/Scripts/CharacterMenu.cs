using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    // Text Fields

    // Logic
    public TMP_Text levelText;
    public TMP_Text xpText;
    public Image treeGrowthSprite;
    public RectTransform xpBar;
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void OnClickOpenInventory()
    {
        anim.SetTrigger("show");
    }

    // Add Entry = Add to Running Total
    public void OnAddEntryClick()
    {
        GameManager.instance.GrantXp(1);
        UpdateMenu();
    }

    public void UpdateMenu()
    {
        levelText.text = "Level: " + (GameManager.instance.GetCurrentLevel() - 1).ToString();

        // xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();

        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString();
            xpBar.localScale = Vector3.one;
        }

        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;
            Debug.Log(diff);
            Debug.Log(currXpIntoLevel);

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            Debug.Log(completionRatio);
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }

        treeGrowthSprite.sprite = GameManager.instance.treeSprites[currLevel - 1];
    }
}
