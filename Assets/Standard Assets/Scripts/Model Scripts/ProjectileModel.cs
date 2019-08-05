using UnityEngine;
using System.Collections;

public class ProjectileModel : MonoBehaviour
{
    // The radius of any area of effect damage/
    float aoeRadius = 0.0f;
    // The damage done by this weapons.
    float damage = 5.0f;
    // The speed at which the projectile of this weapon travels.
    float projectileSpeed = 5.0f;
    // The world position that the projectile should travel to.
    Vector3 targetPos;
    // The transform of this projectile game object.
    Transform thisTransform;

    public float AOERadius
    {
        get { return aoeRadius; }
        set { aoeRadius = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    public Vector3 TargetPos
    {
        get { return targetPos; }
        set { targetPos = value; }
    }

    void Awake()
    {
        thisTransform = transform;
    }

    void Update()
    {
        thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetPos,
            projectileSpeed);
        if (thisTransform.position == targetPos)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        GameCharacterModel target =
            collisionInfo.gameObject.GetComponent<GameCharacterModel>();
        if (target)
        {
            target.ApplyDamage(damage, 0.0f);
        }

        Destroy(gameObject);
    }
}
