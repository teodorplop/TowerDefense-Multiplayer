using UnityEngine;
using System.Collections;

public class TargetedMissile : Missile {
    [HideInInspector]
    public TurretType turretType;

    protected override void OnReachedDestination() {
        damageValue = DamageController.CalculateDamage(damageValue, turretType, targetMonster.Type);
        targetMonster.ApplyDamage(damageValue);
    }
}
