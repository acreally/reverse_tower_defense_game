using UnityEngine;
using System.Collections;

public class DamageOverTimeProjectile : ProjectileController
{
    void Awake()
    {
        ThisTransform = transform;
        Damage = 7.0f;
        PiercingDamage = 0.0f;
        SlowMagnitude = 0.0f;
        SlowDuration = 0.0f;
        AreaOfEffect = 0.0f;
        AreaOfEffectDamage = 0.0f;
        DmgOverTime = 2.0f;
        DmgOverTimeDuration = 5;
    }
}
