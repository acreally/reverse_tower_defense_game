using UnityEngine;
using System.Collections;

public class GameCharacterModel : MonoBehaviour
{
    // The current health of this game character.
    public float currentHealth;
    // The maximum health of this game character.
    protected float maxHealth;
    // The base movement speed of this game character.
    protected float baseSpeed;
    // The current movement speed of this game character. Can change
    // due to debuffs. Used for movement.
    public float currentSpeed;
    // The unaltered amoutn of physical damage that is ignore
    protected float baseDefense;
    // The amount of physical damage that is ignored.
    protected float defense;
    // The number of units in a squad.
    static protected int squadSize = 1;
    // The main camera
    Camera mainCamera;
    // True if this soldier is dead
    bool isDead = false;
    // The number of this squad in the current wave
    protected int squadNumber;
    // True if the soldier can be detected by towers
    public bool visible = true;
    // The delay in seconds between spawns of this type of soldier
    static protected float spawnDelay;
    // True if unique ability has been used
    protected bool abilityUsed = false;
    // The amount of health added for each upgrade point
    public float healthIncreaseAmt;
    // The maximum amount of health after all upgrades
    public float maxHealthAmt;
    // The amount of defense added for each upgrade point
    public float defenseIncreaseAmt;
    // The maximum amount of defense after all upgrades
    public float maxDefenseAmt;
    // The amount of speed added for each upgrade point
    public float speedIncreaseAmt;
    // The maximum amount of speed after all upgrades
    public float maxSpeedAmt;
    // The increase in squad size for each upgrade point
    public int squadSizeIncreaseAmt;
    // The maximum squad size after all upgrades
    public int maxSquadSizeAmt;
    // Texture that displays the current health
    Texture2D healthBar;
    // True if this soldier's health has changed
    protected bool healthChanged = false;
    // The transform component of this game object
    Transform thisTransform;
    // The type of this soldier
    protected string soldierName;
    // True if this soldier has been slowed
    bool slowed = false;

    /*
     * Property for soldierName field.
     */
    public string SoldierName
    {
        get { return soldierName; }
        set { soldierName = value; }
    }

    /*
     * Property for baseDefense field.
     */
    public float BaseDefense
    {
        get { return baseDefense; }
        set { baseDefense = value; }
    }

    /*
     * Property for spawnDelay field.
     */
    public float SpawnDelay
    {
        get { return spawnDelay; }
        set { spawnDelay = value; }
    }

    /*
     * Property for visible field.
     */
    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    /*
     * Property for squadNumber field.
     */
    public int SquadNumber
    {
        get { return squadNumber; }
        set { squadNumber = value; }
    }

    /*
     * Property for currentHealth field.
     */
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    /*
     * Property for maxHealth field.
     */
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    /*
     * Property for baseSpeed field.
     */
    public float BaseSpeed
    {
        get { return baseSpeed; }
        set { baseSpeed = value; }
    }

    /*
     * Property for currentSpeed field.     
     */
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    /*
     * Property for defense field.
     */
    public float Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    /*
     * Property for squadSize field.
     */
    public int SquadSize
    {
        get { return squadSize; }
        set { squadSize = value; }
    }

    public IEnumerator Start()
    {
        thisTransform = transform;
        mainCamera = Camera.main;
        currentSpeed = baseSpeed;
        currentHealth = maxHealth;
        defense = baseDefense;
        spawnDelay = 50.0f / baseSpeed;
        healthBar = new Texture2D(32, 4);
        DrawHealthBar();
        yield return null;
    }

    /*
     * Call a coroutine to slow the currentSpeed of the soldier.
     */
    public void Slow(float magnitude, float duration)
    {
        StartCoroutine(SlowRoutine(magnitude, duration));
    }

    /*
     * Reduce this soldier's speed by magnitude percent for
     * duration seconds.
     */
    IEnumerator SlowRoutine(float magnitude, float duration)
    {
        if (!slowed)
        {
            slowed = true;
            currentSpeed = currentSpeed * (1.0f - magnitude);
            yield return new WaitForSeconds(duration);
            currentSpeed = baseSpeed;
            slowed = false;
        }        
    }

    /*
     * Apply damage to soldier. Do not block any damage.
     */
    public void ApplyDamage(float dmg)
    {
        healthChanged = true;
        currentHealth = Mathf.Clamp(
            currentHealth, 0.0f, currentHealth - dmg);
        DestroyIfDead();        
    }

    /*
     * Decrease current health by dmg. Ignore physicalDefense worth of
     * dmg. Ignore piercingDmg worth of physicalDefense.
     */
    public void ApplyDamage(float dmg, float piercingDmg)
    {
        float adjustedPhyDef = Mathf.Clamp(defense, 0.0f,
            defense - piercingDmg);
        float adjustedDmg = Mathf.Clamp(dmg, 0.0f,
             dmg - adjustedPhyDef);
        ApplyDamage(adjustedDmg);      
    }

    /*
     * Start the damage over time coroutine.
     * seconds.
     */
    public void ApplyDamage(float dmg, int duration)
    {
        StartCoroutine(DamageOverTime(dmg, duration));
    }

    /*
     * Decrease current health by dmg every second for duration
     * seconds.
     */
    IEnumerator DamageOverTime(float dmg, int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            currentHealth = Mathf.Clamp(
                currentHealth, 0.0f, currentHealth - dmg);
            yield return new WaitForSeconds(1.0f);
        }
    }

    /*
     * Destroy the soldier if its health is less than or equal to zero.
     */
    void DestroyIfDead()
    {
        if (currentHealth <= 0.0f && !isDead)
        {
            // If two projectiles hit a soldier at the same time and
            // each shot brings the soldiers health to less than zero
            // then this function will be called twice, but we only want
            // to increment soldiersKilled once
            isDead = true;
            mainCamera.GetComponent<WaveInfo>().IncreaseSoldiersKilled();
            Destroy(gameObject);
        }
    }

    /*
     * Decrease the soldier count when this soldier is killed.
     */
    void OnDestroy()
    {
        if (mainCamera)
        {
            mainCamera.GetComponent<WaveInfo>().DecreaseSoldiersDeployed();
        }        
    }

    /*
     * Draw the health bar above the soldier. Only redraw the health bar
     * if the soldier's health has changed since the last time it was
     * drawn.
     */
    void OnGUI()
    {
        if (thisTransform.name != "Main Camera")
        {
            Vector3 pos = new Vector3(thisTransform.position.x - 13.0f,
            thisTransform.position.y, thisTransform.position.z + 10.0f);
            Vector3 screenPos = mainCamera.WorldToScreenPoint(pos);
            if (healthChanged)
            {
                healthChanged = false;
                DrawHealthBar();
            }
            GUI.DrawTexture(new Rect(screenPos.x, 
                Screen.height - screenPos.y, 32, 4), healthBar);
        }        
    }

    /*
     * Colour in the health bar to reflect the soldier's current
     * health.
     */
    void DrawHealthBar()
    {
        float pivot;
        if (currentHealth > maxHealth)
        {
            pivot = healthBar.width;
        }
        else if (currentHealth <= 0.0f)
        {
            pivot = 0.0f;
        }
        else
        {
            pivot = (currentHealth / maxHealth) * healthBar.width;
        }
        for (int y = 0; y < healthBar.height; y++)
        {
            for (int x = 0; x < healthBar.width; x++)
            {
                if (x <= pivot)
                {
                    healthBar.SetPixel(x, y, Color.green);
                }
                else
                {
                    healthBar.SetPixel(x, y, Color.red);
                }
            }
        }
        healthBar.Apply();
    }

    void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        start.y = transform.lossyScale.y + start.y + 1;
        Vector3 end = transform.position + transform.forward 
            * transform.lossyScale.x;
        end.y = transform.lossyScale.y + end.y + 1;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, end);
    }
}
