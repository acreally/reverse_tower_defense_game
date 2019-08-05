using UnityEngine;
using System.Collections;

public class ExplosiveProjectile : ProjectileController
{
    void Awake()
    {
        ThisTransform = transform;
        Damage = 12.0f;
        PiercingDamage = 0.0f;
        SlowMagnitude = 0.0f;
        SlowDuration = 0.0f;
        AreaOfEffect = 50.0f;
        AreaOfEffectDamage = 8.0f;
    }
}

