using UnityEngine;
using System.Collections;

public class Spy : GameCharacterModel
{
    // Smoke screen prefab
    public GameObject smokeScreen;
    // The amount of health an infantry unit has
    static float health = 90.0f;
    // The speed of an infantry unit
    static float speed = 100.0f;
    // The defense of an infantry unit
    static float def = 3.0f;
    // The size of an infantry squad
    static int sizeOfSquad = 4;
    // The name of this soldier type
    string soldierType = "Spy";

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
        //spawnDelay = 0.5f;
        maxHealthAmt = 135.0f;
        healthIncreaseAmt = 15.0f;
        maxDefenseAmt = 6.0f;
        defenseIncreaseAmt = 1.0f;
        maxSpeedAmt = 160.0f;
        speedIncreaseAmt = 20.0f;
        maxSquadSizeAmt = 7;
        squadSizeIncreaseAmt = 1;
    }
    
    void Update()
    {
        if (Input.GetKeyDown("" + squadNumber) && !abilityUsed)
        {
            abilityUsed = true;
            StartCoroutine(CreateSmokeScreen());
        } 
    }    

    /*
     * Instantiate a smoke screen game object at the spy's current
     * position.
     */
    IEnumerator CreateSmokeScreen()
    {
        gameObject.layer = LayerMask.NameToLayer("Invisible");
        yield return new WaitForSeconds(2.0f);
        gameObject.layer = LayerMask.NameToLayer("Soldiers");
        //Instantiate(smokeScreen, transform.position, transform.rotation);
    }
}
