using UnityEngine;
using System.Collections;
using System;

public class GameInfo : MonoBehaviour 
{
    // Bit mask that keeps track of what levels the player has unlocked
    static int levelsUnlocked = 1;
    // The script that controls the waves of soldiers
    WaveInfo waveInfo;
    // The script that controls the level select process
    LevelSelect levelSelect;
    // The script that controls the stats increase process
    IncreaseStats increaseStats;
    // Loading screen script
    LoadingScreen loadingScreen;
    // The amount of points the player can spend on upgrades
    static int[] upgradePoints = new int[5];
    // The goal to reach for each level
    int[] levelGoals = { 25, 30, 35, 40, 50 };
    // The maximum number of soldiers that can be killed each level
    int[] levelMaxKilled = { 100, 90, 80, 70, 50 };

    /*
     * Property for levelsUnlocked field.
     */
    public int LevelsUnlocked
    {
        get { return levelsUnlocked; }
        set { levelsUnlocked = value; }
    }

    /*
     * Property for levelGoals field.
     */
    public int[] LevelGoals
    {
        get { return levelGoals; }
        set { levelGoals = value; }
    }

    /*
     * Property for levelMaxKilled field.
     */
    public int[] LevelMaxKilled
    {
        get { return levelMaxKilled; }
        set { levelMaxKilled = value; }
    }

    /*
     * Property for upgradePoints field.
     */
    public int[] UpgradePoints
    {
        get { return upgradePoints; }
        set { upgradePoints = value; }
    }

    void Awake()
    {
        GameObject.DontDestroyOnLoad(Camera.main);
        GameObject.DontDestroyOnLoad(
            GameObject.FindGameObjectWithTag("Light"));
        waveInfo = GetComponent<WaveInfo>();
        levelSelect = GetComponent<LevelSelect>();
        increaseStats = GetComponent<IncreaseStats>();
        loadingScreen = GetComponent<LoadingScreen>();
        Application.LoadLevel("levelSelect");  
    }

    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName == "levelSelect")
        {
            levelSelect.enabled = true;
            waveInfo.enabled = false;
            increaseStats.enabled = false;
            loadingScreen.enabled = false;
            
        }
        else if (Application.loadedLevelName == "increaseStats")
        {
            levelSelect.enabled = false;
            waveInfo.enabled = false;
            increaseStats.enabled = true;
            loadingScreen.enabled = false;
        }
        else if (Application.loadedLevelName == "loading")
        {
        }
        else
        {
            levelSelect.enabled = false;
            waveInfo.enabled = true;
            increaseStats.enabled = false;
            loadingScreen.enabled = false;
            waveInfo.Score = 0;
            waveInfo.SoldiersKilled = 0;
            int levelNumber = Convert.ToInt32(Application.loadedLevelName);
            waveInfo.Goal = levelGoals[levelNumber - 1];
            waveInfo.MaxKilled = levelMaxKilled[levelNumber - 1];
            waveInfo.SpawnTransform =
                GameObject.FindGameObjectWithTag("Spawn").transform;
            waveInfo.LevelDone = false;
            waveInfo.LevelNumber = levelNumber;
        }
    }
}
