using UnityEngine;
using System.Collections;

public class Mage : GameCharacterModel
{
    // The distance in which towers should be slowed
    float slowDistance = 150.0f;
    // The percentage to slow towers (percentage of slow effect is
    // actually 1 - (1 / slowMagnitude))
    float slowMagnitude = 1.33f;
    // The time in seconds to slow towers
    float slowDuration = 3.0f;
    // The amount of health an infantry unit has
    static float health = 80.0f;
    // The speed of an infantry unit
    static float speed = 60.0f;
    // The defense of an infantry unit
    static float def = 2.0f;
    // The size of an infantry squad
    static int sizeOfSquad = 5;
    // The name of this soldier type
    string soldierType = "Mage";

    /*
     * Property for soldierType field.
     */
    public string SoldierType
    {
        get { return soldierType; }
        set { soldierType = value; }
    }

    /*
     * Property for health field.
     */
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    /*
     * Property for speed field.
     */
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    /*
     * Property for def field. 
     */
    public float Def
    {
        get { return def; }
        set { def = value; }
    }

    /*
     *  Property for sizeOfSquad field
     */
    public int SizeOfSquad
    {
        get { return sizeOfSquad; }
        set { sizeOfSquad = value; }
    }

    void Awake()
    {
        maxHealth = health;
        baseSpeed = speed;
        baseDefense = def;
        squadSize = sizeOfSquad;
        soldierName = soldierType;
        //spawnDelay = 0.65f;
        maxHealthAmt = 110.0f;
        healthIncreaseAmt = 10.0f;
        maxDefenseAmt = 5.0f;
        defenseIncreaseAmt = 1.0f;
        maxSpeedAmt = 90.0f;
        speedIncreaseAmt = 10.0f;
        maxSquadSizeAmt = 8;
        squadSizeIncreaseAmt = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown("" + squadNumber) && !abilityUsed)
        {
            abilityUsed = true;
            SlowTowers();
        }       
    }

    /*
     * Slow the rate of fire of towers in range for 3 seconds.
     */
    void SlowTowers()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towers)
        {
            if (Vector3.Distance(
                transform.position, tower.transform.position) <=
                slowDistance)
            {
                tower.GetComponent<TowerController>().Slow(
                    slowMagnitude, slowDuration);
            }
        }
    }
}