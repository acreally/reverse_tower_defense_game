using UnityEngine;
using System.Collections;

public class Knight : GameCharacterModel
{
    // The amount of health an infantry unit has
    static float health = 150.0f;
    // The speed of an infantry unit
    static float speed = 45.0f;
    // The defense of an infantry unit
    static float def = 8.0f;
    // The size of an infantry squad
    static int sizeOfSquad = 5;
    // The name of this soldier type
    string soldierType = "Knight";

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
        //spawnDelay = 0.8f;
        maxHealthAmt = 300.0f;
        healthIncreaseAmt = 50.0f;
        maxDefenseAmt = 14.0f;
        defenseIncreaseAmt = 2.0f;
        maxSpeedAmt = 60.0f;
        speedIncreaseAmt = 5.0f;
        maxSquadSizeAmt = 8;
        squadSizeIncreaseAmt = 1;
    }
   
    void Update()
    {
        if (Input.GetKeyDown("" + squadNumber) && !abilityUsed)
        {
            abilityUsed = true;
            StartCoroutine(DefenseUp());
        }
    }    

    /*
     * Increase the defense statistic of this knight.
     */
    IEnumerator DefenseUp()
    {
        defense = baseDefense * 1.3f;
        yield return new WaitForSeconds(3.0f);
        defense = baseDefense;
    }
}