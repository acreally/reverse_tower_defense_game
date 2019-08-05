using UnityEngine;
using System.Collections;

public class Infantry : GameCharacterModel 
{
    // The amount of health an infantry unit has
    static float health = 100.0f;
    // The speed of an infantry unit
    static float speed = 70.0f;
    // The defense of an infantry unit
    static float def = 5.0f;
    // The size of an infantry squad
    static int sizeOfSquad = 6;
    // The name of this soldier type
    string soldierType = "Infantry";

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
        maxHealthAmt = 175.0f;
        healthIncreaseAmt = 25.0f;
        maxDefenseAmt = 8.0f;
        defenseIncreaseAmt = 1.0f;
        maxSpeedAmt = 115.0f;
        speedIncreaseAmt = 15.0f;
        maxSquadSizeAmt = 12;
        squadSizeIncreaseAmt = 2;
	}
	    
	void Update() 
    {
        if (Input.GetKeyDown("" + squadNumber)
            && !abilityUsed)
        {
            abilityUsed = true;
            StartCoroutine(SpeedUp());
        }
	}     

    /*
     * Increase the speed of this infantry unit for a short duration.
     */
    IEnumerator SpeedUp()
    {
        currentSpeed = currentSpeed * 1.5f;
        yield return new WaitForSeconds(2.5f);
        currentSpeed = baseSpeed;
    }
}
