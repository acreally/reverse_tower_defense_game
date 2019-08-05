using UnityEngine;
using System.Collections;
using System;

public class WaveInfo : MonoBehaviour {
    // Healer prefab
    public GameObject healer;
    // Infantry prefab
    public GameObject infantry;
    // Knight prefab
    public GameObject knight;
    // Mage prefab
    public GameObject mage;
    // Spy prefab
    public GameObject spy;
    // Array of squads to be deployed in the wave
    GameObject[] squads = new GameObject[3];
    // The transform of the spawn point
    Transform spawnTransform;
    // True if the squads have been selected and it is time to start
    // the wave
    bool startWave = false;
    // How many soldiers have made it through the maze so far
    int score = 0;
    // How many soldiers have been deployed in the current wave
    public int soldiersDeployed = 0;
    // How many soldiers have been killed in the current level.
    int soldiersKilled = 0;
    // True when there are no more soldiers left from the current wave.
    bool waveDone = false;
    // True when all the squads have been spawned for a wave
    bool allSquadsSpawned = false;
    // True if the first squad should be spawned
    bool spawnFirstSquad = false;
    // True if the second squad should be spawned
    bool spawnSecondSquad = false;
    // True if the Third squad should be spawned
    bool spawnThirdSquad = false;    
    // The style for the healer selection button
    public GUIStyle healerButtonStyle;
    // The style for the infantry selection button
    public GUIStyle infantryButtonStyle;
    // The style for the knight selection button
    public GUIStyle knightButtonStyle;
    // The style for the mage selection button
    public GUIStyle mageButtonStyle;    
    // The style for the spy selection button
    public GUIStyle spyButtonStyle;
    // The style for the disabled healer selection button
    public GUIStyle healerBoxStyle;
    // The style for the disbaled infantry selection button
    public GUIStyle infantryBoxStyle;
    // The style for the disabled knight selection button
    public GUIStyle knightBoxStyle;
    // The style for the disabled mage selection button
    public GUIStyle mageBoxStyle;    
    // The style for the disabled spy selection button
    public GUIStyle spyBoxStyle;
    // The style for the top GUI section
    public GUIStyle topGUIStyle;
    // The style for the bottom GUI section
    public GUIStyle bottomGUIStyle;
    // The style for the left GUI section
    public GUIStyle leftGUIStyle;
    // The style for the right GUI section
    public GUIStyle rightGUIStyle;
    // The style for score and killed boxes
    public GUIStyle textBoxGUIStyle;
    // GUI style for a blank box
    public GUIStyle blankStyle;
    // True if the player is choosing the first squad
    bool selectFirstSquad = true;
    // True if the player is choosing the second squad
    bool selectSecondSquad = false;
    // True if the player is choosing the third squad
    bool selectThirdSquad = false;
    // The GUI style for the first squad
    GUIStyle firstSquadStyle;
    // The GUI style for the second squad
    GUIStyle secondSquadStyle;
    // The GUI style for the third squad
    GUIStyle thirdSquadStyle;
    // The GUI style for the start button
    public GUIStyle startButton;
    // The GUI style for the diabled start button
    public GUIStyle startButtonDisabled;
    // True when all the squads have been selected and the wave is ready
    // to begin
    bool readyToStartWave = false;
    // GUI style for a blank icon (no squad selected)
    public GUIStyle blankIcon;
    // The maximum amount of soldiers that can be killed before the
    // player loses the level
    int maxKilled;
    // The score the player needs to reach to complete the level
    int goal;
    // The script that contains information that needs to be accessed
    // across levels
    GameInfo gameInfo;
    // True if the level is finished
    bool levelDone = false;
    // The number of the current level
    int levelNumber;
    // The most recently selected squad
    GameObject selectedSquad;
    // The range of the selected tower
    int towerRange;
    // The rate of fire of the selected tower
    float towerRoF;
    // The regular damage of the selected tower
    float towerDamage;
    // The piercing damage of the selected tower
    float towerPiercingDamage;
    // The area of effect of the selected tower
    float towerAoE;
    // The area of effect damage of the selected tower
    float towerAoEDamage;
    // The slow effect of the selected tower
    float towerSlow;
    // The duration of the slow effect of the selected tower
    float towerSlowDuration;
    // The damage over time of the selected tower
    float towerDoT;
    // The duration of the damage over time of the selected tower
    float towerDoTDuration;
    // The health of the selected squad
    float squadHealth;
    // The defense of the selected squad
    float squadDefense;
    // The speed of the selected squad
    float squadSpeed;
    // The size of the selected squad
    int squadSize;
    // The name of the selected squad
    string squadName;

    /*
     * Property for levelNumber field.
     */
    public int LevelNumber
    {
        get { return levelNumber; }
        set { levelNumber = value; }
    }

    /*
     * Property for levelDone field.
     */
    public bool LevelDone
    {
        get { return levelDone; }
        set { levelDone = value; }
    }

    /*
     * Property for score field
     */
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    /*
     * Property for goal field.
     */
    public int Goal
    {
        get { return goal; }
        set { goal = value; }
    }

    /*
     * Property for soldiersKilled field.
     */
    public int SoldiersKilled
    {
        get { return soldiersKilled; }
        set { soldiersKilled = value; }
    }

    /*
     * Property for maxKilled field.
     */
    public int MaxKilled
    {
        get { return maxKilled; }
        set { maxKilled = value; }
    }

    /*
     * Property for spawnTransform field.
     */
    public Transform SpawnTransform
    {
        get { return spawnTransform; }
        set { spawnTransform = value; }
    }

    void OnEnable()
    {
    }

	void Start() 
    {
        if (Application.loadedLevelName != "levelSelect" && 
            Application.loadedLevelName != "increaseStats" &&
            Application.loadedLevelName != "loading")
        {
            levelNumber = Convert.ToInt32(Application.loadedLevelName);
        }        
        gameInfo = GetComponent<GameInfo>();
        
	}

    void FixedUpdate()
    {
        if (startWave)
        {
            startWave = false;
            spawnFirstSquad = true;
            StartCoroutine(SpawnSquads());
        }
    }

	void Update() 
    {        
        if (waveDone)
        {
            waveDone = false;
            selectFirstSquad = true;
            squads = new GameObject[3] {null, null, null};
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;            
            if (Physics.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition), 
                Vector3.up * -1.0f, out hitInfo, 1 <<
                LayerMask.NameToLayer("Tower"))) 
            {
                GameObject tower = hitInfo.collider.gameObject;
                TowerController towerScript = tower.GetComponent<
                    TowerController>();
                GameObject projectile = (GameObject)Instantiate(
                    towerScript.projectile, new Vector3(
                        0.0f, -10.0f, 0.0f), Quaternion.identity);
                ProjectileController projectileScript = projectile.GetComponent<
                    ProjectileController>();
                towerAoE = projectileScript.AreaOfEffect;
                towerAoEDamage = projectileScript.AreaOfEffectDamage;
                towerDamage = projectileScript.Damage;
                towerDoT = projectileScript.DmgOverTime;
                towerDoTDuration = projectileScript.DmgOverTimeDuration;
                towerPiercingDamage = projectileScript.PiercingDamage;
                towerRange = towerScript.FireRange;
                towerRoF = towerScript.RateOfFire;
                towerSlow = projectileScript.SlowMagnitude;
                towerSlowDuration = projectileScript.SlowDuration;
                Destroy(projectile);
            }
        }
	}

    /*
     * Spawn the three squads for this wave.
     */
    private IEnumerator SpawnSquads()
    {    
        if (spawnFirstSquad)
        {
            yield return StartCoroutine(SpawnSquad(squads[0], 1));
            spawnFirstSquad = false;
            spawnSecondSquad = true;
            //yield return new WaitForSeconds(50.0f / squads[0].GetComponent<
            //    GameCharacterModel>().BaseSpeed);
        }
        if (spawnSecondSquad)
        {
            yield return StartCoroutine(SpawnSquad(squads[1], 2));
            spawnSecondSquad = false;
            spawnThirdSquad = true;
            //yield return new WaitForSeconds(50.0f / squads[1].GetComponent<
            //    GameCharacterModel>().BaseSpeed);
        }
        if (spawnThirdSquad)
        {
            yield return StartCoroutine(SpawnSquad(squads[2], 3));
            spawnThirdSquad = false;
            allSquadsSpawned = true;  
        }          
    }

    /*
     * Spawn all the soldiers in a particular squad.
     */
    IEnumerator SpawnSquad(GameObject squad, int squadNum)
    {        
        GameCharacterModel soldierScript =
                squad.GetComponent<GameCharacterModel>();
        int soldiersSpawned = 0;
        while (soldiersSpawned < soldierScript.SquadSize)
        {
            Vector3 startPos = spawnTransform.position;
            startPos.y = startPos.y + squad.transform.lossyScale.y;
            GameObject newSoldier = (GameObject)Instantiate(
                squad, startPos, Quaternion.identity);           
            GameCharacterModel newSoldierScript =
                newSoldier.GetComponent<GameCharacterModel>();
            newSoldierScript.SquadNumber = squadNum;            
            yield return StartCoroutine(newSoldierScript.Start());
            soldiersSpawned++;
            soldiersDeployed++;
            yield return new WaitForSeconds(newSoldierScript.SpawnDelay);
        }        
    }

    /*
     * Decrease soldiers deployed when a soldier is killed or makes it to
     * the end of the level.
     */
    public void DecreaseSoldiersDeployed()
    {
        soldiersDeployed = soldiersDeployed - 1;
        // Player has successfully completed the level
        if (score == goal && !levelDone)
        {
            levelDone = true;
            if ((gameInfo.LevelsUnlocked & 1 << levelNumber) == 0)
            {
                int[] upgradePoints = gameInfo.UpgradePoints;
                for (int i = 0; i < upgradePoints.Length; i++)
                {
                    upgradePoints[i] += 3;
                }
                gameInfo.UpgradePoints = upgradePoints;
            }
            gameInfo.LevelsUnlocked = gameInfo.LevelsUnlocked |
                1 << levelNumber;            
            Application.LoadLevel("increaseStats");
        }
        // Player failed the level
        else if (soldiersKilled == maxKilled && !levelDone)
        {
            levelDone = true;
            Application.LoadLevel("intro");
        }
        if (allSquadsSpawned && soldiersDeployed == 0)
        {
            allSquadsSpawned = false;
            waveDone = true;
        }
    }

    /*
     * Increase soldiersKilled field when a soldier is killed by a tower.
     */
    public void IncreaseSoldiersKilled()
    {
        soldiersKilled++;
    }

    /*
     * Increase the score by one when a soldier makes it to the last
     * waypoint.
     */
    public void RaiseScore()
    {
        score++;
    }

    void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Box(new Rect(0, 0, 210, Screen.height), "", leftGUIStyle);        
        GUI.Box(new Rect(210, 0, 604, 109), "Selected Squads", 
            topGUIStyle);       
        GUI.Box(new Rect(210, 659, 604, 109), "Choose your squads",
            bottomGUIStyle);
        GUI.Box(new Rect(814, 0, 210, Screen.height), "", rightGUIStyle);        
        // Score box
        GUI.Box(new Rect(672, 10, 128, 32), "Score: " + score + "/" +
            goal, textBoxGUIStyle);
        // Soldiers killed this wave box
        GUI.Box(new Rect(672, 58, 128, 32), "Killed: " + soldiersKilled 
            + "/" + maxKilled, textBoxGUIStyle);

        // Start button: press to start current wave
        if (readyToStartWave)
        {
            if (GUI.Button(new Rect(650, Screen.height - 80, 128, 64), "",
                startButton))
            {
                readyToStartWave = false;
                startWave = true;
            }
        }
        else
        {
            GUI.Box(new Rect(650, Screen.height - 80, 128, 64), "",
                startButtonDisabled);
        }
        // Selected squads buttons
        if (!squads[0])
        {
            GUI.Box(new Rect(220, 30, 64, 64), "", blankIcon);
        }
        else
        {
            if (!spawnFirstSquad && !spawnSecondSquad && !spawnThirdSquad &&
                !allSquadsSpawned)
            {
                if (GUI.Button(new Rect(220, 30, 64, 64), "1", 
                    firstSquadStyle))
                {
                    squads[0] = null;
                    selectFirstSquad = true;
                    readyToStartWave = false;
                }
            } 
            else 
            {
                GUI.Box(new Rect(220, 30, 64, 64), "1", firstSquadStyle);
            }
        }
        if (!squads[1])
        {
            GUI.Box(new Rect(300, 30, 64, 64), "", blankIcon);
        }
        else
        {
            if (!spawnFirstSquad && !spawnSecondSquad && !spawnThirdSquad &&
                !allSquadsSpawned)
            {
                if (GUI.Button(new Rect(300, 30, 64, 64), "2",
                secondSquadStyle))
                {
                    squads[1] = null;
                    selectSecondSquad = true;
                    readyToStartWave = false;
                }
            }
            else
            {
                GUI.Box(new Rect(300, 30, 64, 64), "2", secondSquadStyle);
            }
            
        }
        if (!squads[2])
        {
            GUI.Box(new Rect(380, 30, 64, 64), "", blankIcon);
        }
        else
        {
            if (!spawnFirstSquad && !spawnSecondSquad && !spawnThirdSquad &&
                !allSquadsSpawned)
            {
                if (GUI.Button(new Rect(380, 30, 64, 64), "3",
                thirdSquadStyle))
                {
                    squads[2] = null;
                    selectThirdSquad = true;
                    readyToStartWave = false;
                }
            }
            else
            {
                GUI.Box(new Rect(380, 30, 64, 64), "3", thirdSquadStyle);
            }
        }
        // Squad selection buttons
        if (selectFirstSquad || selectSecondSquad || selectThirdSquad)
        {
            if (GUI.Button(new Rect(
                220, Screen.height - 80, 64, 64), "", infantryButtonStyle))
            {
                AssignSquad(infantry, infantryButtonStyle);
            }
            if (GUI.Button(new Rect(
                300, Screen.height - 80, 64, 64), "", knightButtonStyle))
            {
                AssignSquad(knight, knightButtonStyle);
            }
            if (GUI.Button(new Rect(
                380, Screen.height - 80, 64, 64), "", mageButtonStyle))
            {
                AssignSquad(mage, mageButtonStyle);
            }
            if (GUI.Button(new Rect(
                460, Screen.height - 80, 64, 64), "", healerButtonStyle))
            {
                AssignSquad(healer, healerButtonStyle);
            }
            if (GUI.Button(new Rect(
                540, Screen.height - 80, 64, 64), "", spyButtonStyle))
            {
                AssignSquad(spy, spyButtonStyle);
            }
        }
        // Disable squad selection buttons once wave has started
        else
        {
            GUI.Box(new Rect(
                220, Screen.height - 80, 64, 64), "", infantryBoxStyle);
            GUI.Box(new Rect(
                300, Screen.height - 80, 64, 64), "", knightBoxStyle);
            GUI.Box(new Rect(
                380, Screen.height - 80, 64, 64), "", mageBoxStyle);
            GUI.Box(new Rect(
                460, Screen.height - 80, 64, 64), "", healerBoxStyle);
            GUI.Box(new Rect(
                540, Screen.height - 80, 64, 64), "", spyBoxStyle);
        }
        int height = 24;
        int width = 160;
        int towerInfoLeft = 25;
        int squadInfoLeft = 839;
        // Display information about selected squad
        GUI.Box(new Rect(squadInfoLeft, 40, width, 32), "Squad Info", 
            textBoxGUIStyle);
        GUI.Box(new Rect(squadInfoLeft, 72, width, height), "Name: " +
            squadName, textBoxGUIStyle);
        GUI.Box(new Rect(squadInfoLeft, 94, width, height), "Health: " +
            squadHealth, textBoxGUIStyle);
        GUI.Box(new Rect(squadInfoLeft, 118, width, height), 
            "Defense: " +  squadDefense, textBoxGUIStyle);
        GUI.Box(new Rect(squadInfoLeft, 142, width, height), "Speed: " +
            squadSpeed, textBoxGUIStyle);
        GUI.Box(new Rect(squadInfoLeft, 166, width, height), 
            "Squad Size: " + squadSize, textBoxGUIStyle);
        // Display information about tower
        GUI.Box(new Rect(towerInfoLeft, 40, width, 32), "Tower Info", 
            textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 72, width, height), 
            "Regular Damage: " + towerDamage, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 94, width, height), 
            "Piercing Damage: " + towerPiercingDamage, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 118, width, height), 
            "Area of Effect: " + towerAoE, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 142, width, height), 
            "AoE Damage: " + towerAoEDamage, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 166, width, height), 
            "Slow Effect: " + towerSlow * 100.0f + "%", textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 190, width, height), 
            "Slow Duration: " + towerSlowDuration, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 214, width, height), 
            "Damage over Time: " + towerDoT, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 238, width, height), 
            "DoT Duration: " + towerDoTDuration, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 262, width, height), "Range: " + 
            towerRange, textBoxGUIStyle);
        GUI.Box(new Rect(towerInfoLeft, 286, width, height), 
            "Rate of Fire: " + towerRoF, textBoxGUIStyle);                  
    }

    /*
     * Assign a squad of soldiers to the appropriate index in the array.
     */
    void AssignSquad(GameObject squad, GUIStyle style)
    {
        if (selectFirstSquad)
        {
            selectFirstSquad = false;
            squads[0] = squad;
            firstSquadStyle = style;
            GetNextState();
        }
        else if (selectSecondSquad)
        {
            selectSecondSquad = false;
            squads[1] = squad;
            secondSquadStyle = style;
            GetNextState();
        }
        else if (selectThirdSquad)
        {
            selectThirdSquad = false;
            squads[2] = squad;
            thirdSquadStyle = style;
            GetNextState();
        }
        selectedSquad = squad;
        GetSquadStats(selectedSquad);
    }

    /*
     * Get the stats of the most recently selected squad to display in the
     * GUI.
     */
    void GetSquadStats(GameObject squad) 
    {
        GameObject newSquad = (GameObject)Instantiate(
                squad, new Vector3(-50.0f, -200.0f, 0.0f),
                Quaternion.identity);
        GameCharacterModel soldierScript = newSquad.GetComponent<
            GameCharacterModel>();
        squadDefense = soldierScript.BaseDefense;
        squadHealth = soldierScript.MaxHealth;
        squadSize = soldierScript.SquadSize;
        squadSpeed = soldierScript.BaseSpeed;
        squadName = soldierScript.SoldierName;
        Destroy(newSquad);
    }

    /*
     * Determine which state the game should be in after assigning a squad.
     */
    void GetNextState()
    {
        int i = 0;
        int nextSquad = -1;
        while (i < squads.Length)
        {            
            if (!squads[i])
            {                
                break;
            }
            i++;
        }
        nextSquad = i + 1;
        if (nextSquad == 1)
        {
            selectFirstSquad = true;
        }
        else if (nextSquad == 2)
        {
            selectSecondSquad = true;
        }
        else if (nextSquad == 3)
        {
            selectThirdSquad = true;
        }
        else if (nextSquad == 4)
        {
            readyToStartWave = true;
        }
        else
        {
            Debug.Log("Error while assigning squads");
        }
    }
}
