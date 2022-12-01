using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldEnvironment : MonoBehaviour
{
    static WorldEnvironment instance;
    public Volume ppv;
    private int currentLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ppv = gameObject.GetComponent<Volume>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentLevel = GameManager.instance.GetCurrentLevel() - 1;
        Debug.Log("current: " + currentLevel);

        ControlPPV();
    }

    public void ControlPPV() // used to adjust the post processing slider.
    {
        if (currentLevel == 1)
        {
            ppv.weight = (float)0.8;
        }

        if (currentLevel == 2)
        {
            ppv.weight = (float)0.6;
        }
    }
}
