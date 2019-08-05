using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
    // The base damage caused by this projectile
    float damage;
    // The amount of physical defense that is ignored
    float piercingDmg;
    // The target of this projectile
    Transform target;
    // The transform component of this game object
    Transform thisTransform;
    // The magnitude of the slowing effect of this projectile
    float slowMagnitude;
    // The duration of the slowing effect of this projectile
    float slowDuration;
    // The area in which soldiers should receive damage
    float areaOfEffect;
    // The damage in the area of effect
    float areaOfEffectDamage;
    // The damage of any DoT effects
    float dmgOverTime;
    // The duration of any DoT effects
    int dmgOverTimeDuration;

    /*
     * Property for thisTransfrom field.
     */
    public Transform ThisTransform
    {
        get { return thisTransform; }
        set { thisTransform = value; }
    }

    /*
     * Property for dmgOverTime field
     */
    public float DmgOverTime
    {
        get { return dmgOverTime; }
        set { dmgOverTime = value; }
    }

    /*
     * Property for dmgOverTimeDuration field.
     */
    public int DmgOverTimeDuration
    {
        get { return dmgOverTimeDuration; }
        set { dmgOverTimeDuration = value; }
    }

    /*
     * Property for areaOfEffect field.
     */
    public float AreaOfEffect
    {
        get { return areaOfEffect; }
        set { areaOfEffect = value; }
    }

    /*
     * Property for areaOfEffectDamage field.
     */
    public float AreaOfEffectDamage
    {
        get { return areaOfEffectDamage; }
        set { areaOfEffectDamage = value; }
    }

    /*
     * Property for slowMagnitude field.
     */
    public float SlowMagnitude
    {
        get { return slowMagnitude; }
        set { slowMagnitude = value; }
    }

    /*
     * Property for slowDuration field.
     */
    public float SlowDuration
    {
        get { return slowDuration; }
        set { slowDuration = value; }
    }
    
    /*
     * Property for target field
     */
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    /*
     * Property for damage field. 
     */
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    /*
     * Property for piercingDmg field. 
     */
    public float PiercingDamage
    {
        get { return piercingDmg; }
        set { piercingDmg = value; }
    }
	
    /*
     * Move towards the current target. Destroy this projectile
     * if it has no target, since that means its target has been
     * destroyed.
     */
	void Update() 
    {
        if (target)
        {
            thisTransform.position =
                Vector3.MoveTowards(thisTransform.position,
                target.position, 500.0f * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }        
	}

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 
            LayerMask.NameToLayer("Soldiers"))
        {
            // Apply regular damage
            GameCharacterModel soldierScript =
                collisionInfo.gameObject.GetComponent<
                GameCharacterModel>();
            soldierScript.ApplyDamage(damage, piercingDmg);
            // Apply area of effect damage
            if (areaOfEffect > 0.0f)
            {
                Collider[] hits = Physics.OverlapSphere(
                    thisTransform.position, areaOfEffect,
                    1 << LayerMask.NameToLayer("Soldiers"));
                foreach (Collider soldier in hits)
                {
                    soldier.transform.gameObject.GetComponent<
                        GameCharacterModel>().ApplyDamage(
                        areaOfEffectDamage);
                }
            }
            // Apply damage over time effect
            if (dmgOverTimeDuration > 0)
            {
                soldierScript.ApplyDamage(dmgOverTime, dmgOverTimeDuration);
            }
            // Apply slow effect
            if (slowMagnitude > 0.0f)
            {
                soldierScript.Slow(slowMagnitude, slowDuration);
            }            
            Destroy(gameObject, 0.1f);
        }        
    }

    void OnGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 75.0f);
    }
}
