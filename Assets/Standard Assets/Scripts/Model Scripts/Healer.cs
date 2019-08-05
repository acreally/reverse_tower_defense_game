using UnityEngine;
using System.Collections;

public class Healer : GameCharacterModel
{
    // The amount of health an infantry unit has
    static float health = 75.0f;
    // The speed of an infantry unit
    static float speed = 70.0f;
    // The defense of an infantry unit
    static float def = 2.0f;
    // The size of an infantry squad
    static int sizeOfSquad = 5;
    // The name of this soldier type
    string soldierType = "Healer";
    

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
        maxHealthAmt = 135.0f;
        healthIncreaseAmt = 20.0f;
        maxDefenseAmt = 5.0f;
        defenseIncreaseAmt = 1.0f;
        maxSpeedAmt = 100.0f;
        speedIncreaseAmt = 10.0f;
        maxSquadSizeAmt = 8;
        squadSizeIncreaseAmt = 1;
    }
    
    void Update()
    {
        if (Input.GetKeyDown("" + squadNumber)
            && !abilityUsed)
        {
            abilityUsed = true;
            StartCoroutine(RestoreHealth());
        }
    }    

    /*
     * Restore a percentage of healer's maximum health every second
     * for 5 seconds. Can overheal.
     */
    IEnumerator RestoreHealth()
    {
        
        for (int i = 0; i < 5; i++)
        {
            float healthRestored = maxHealth * 0.06f + currentHealth;
            currentHealth = healthRestored;
            healthChanged = true;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
