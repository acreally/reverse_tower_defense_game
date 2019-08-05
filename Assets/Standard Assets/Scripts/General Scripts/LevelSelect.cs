using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour 
{
    // The script that contains information that needs to be accessed
    // across levels
    GameInfo gameInfo;
    //GUI style for background
    public GUIStyle backgroundStyle;
    // GUI style for the level locked button
    public GUIStyle levelLockedStyle;
    // GUI style for level 1 select button
    public GUIStyle levelButtonStyle;

    void Awake()
    {
        DontDestroyOnLoad(Camera.main);
        if (Application.loadedLevelName == "levelSelect")
        {
            GetComponent<WaveInfo>().enabled = false;
            GetComponent<IncreaseStats>().enabled = false;
        }
        else if (Application.loadedLevelName == "increaseStats")
        {
            GetComponent<IncreaseStats>().enabled = true;
            GetComponent<WaveInfo>().enabled = false;
            this.enabled = false;
        }
        else
        {
            GetComponent<IncreaseStats>().enabled = false;
            GetComponent<WaveInfo>().enabled = true;
            this.enabled = false;
        }
    }

    void Start()
    {
        gameInfo = GetComponent<GameInfo>();
    }
    
    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "",
            backgroundStyle);
        if (GUI.Button(new Rect(400, 124, 256, 64), "1", levelButtonStyle))
        {
            Application.LoadLevel("1");
        }
        int height = 188;
        for (int i = 1; i < 5; i++)
        {
            if ((1 << i & gameInfo.LevelsUnlocked) != 0)
            {
                if (GUI.Button(new Rect(400, height, 256, 64), "" + (i + 1), 
                    levelButtonStyle))
                {
                    Application.LoadLevel("" + (i + 1));
                }
            }
            else
            {
                GUI.Box(new Rect(400, height, 256, 64), "", 
                    levelLockedStyle);
            }
            height = height + 64;
        }
        
    }
}
