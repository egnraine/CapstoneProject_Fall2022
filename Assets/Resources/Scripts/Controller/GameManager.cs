using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        prevIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Resources
    public List<Sprite> treeSprites;
    public List<int> xpTable;

    // References
    public Player player;
    public DialogueManager dialogueManager;
    //public FloatingTextManager floatingTextManager;

    // Logic
    public int coins;
    public int experience;

    static int prevIndex;

    // Floating Text -- Errors
    /*public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }*/

    // Tree Growth | Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max Level
                return r;
        }
        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        Debug.Log("Level Up!");
    }

    // Save Game State
    /*
     * INT coins
     * INT experience
     */

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += experience.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    // Load Game State
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        Debug.Log("LoadState");
        if (PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change coins
        // coins = int.Parse(data[0]);

        // Change experience
        // experience = int.Parse(data[1]);

        if (prevIndex != 2 && SceneManager.GetActiveScene().buildIndex == 1) // if previous scene does not equal "Farm" & current scene equals "Home"
        {
            GameObject.Find("Player").transform.position = GameObject.Find("SpawnPoint").transform.position;
        }

        else if (prevIndex == 1 || prevIndex == 2) // if previous scene equals "Home" or "Farm"
        {
            GameObject.Find("Player").transform.position = GameObject.Find("LoadPoint").transform.position;
        }
    }
}
