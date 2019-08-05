using UnityEngine;
using System.Collections;

public class RegularProjectile : ProjectileController
{
    void Awake()
    {
        ThisTransform = transform;
        Damage = 20.0f;
        PiercingDamage = 0.0f;
        SlowMagnitude = 0.0f;
        SlowDuration = 0.0f;
        AreaOfEffect = 0.0f;
        AreaOfEffectDamage = 0.0f;
    }
}
