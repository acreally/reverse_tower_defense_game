using UnityEngine;
using System.Collections;

public class SlowingProjectile : ProjectileController
{
    void Awake()
    {
        ThisTransform = transform;        
        Damage = 10.0f;
        PiercingDamage = 0.0f;
        SlowMagnitude = 0.25f;
        SlowDuration = 1.0f;
        AreaOfEffect = 0.0f;
        AreaOfEffectDamage = 0.0f;
    }
}

