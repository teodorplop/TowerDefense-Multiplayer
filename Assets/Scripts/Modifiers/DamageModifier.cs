using UnityEngine;
using System.Collections;

public abstract class DamageModifier {

    public abstract bool IsApplicable(TurretType turretType, MonsterType monsterType);
    public abstract float CalculateDamage(TurretType turretType, MonsterType monsterType, float currentValue);
}
