using UnityEngine;
using System.Collections;

public class PiercingProjectile : ProjectileController
{
    void Awake()
    {
        ThisTransform = transform;
        Damage = 15.0f;
        PiercingDamage = 10.0f;
        SlowMagnitude = 0.0f;
        SlowDuration = 0.0f;
        AreaOfEffect = 0.0f;
        AreaOfEffectDamage = 0.0f;
    }
}
