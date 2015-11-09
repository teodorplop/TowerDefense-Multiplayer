using UnityEngine;
using System.Collections;

public class ElfTowerModifier : DamageModifier {
    public override bool IsApplicable(TurretType turretType, MonsterType monsterType) {
        if (turretType == TurretType.Piercing) {
            return true;
        }
        return false;
    }

    public override float CalculateDamage(TurretType turretType, MonsterType monsterType, float currentValue) {
        if (monsterType == MonsterType.Mage) {
            return currentValue * 0.3f;
        }
        if (monsterType == MonsterType.ADC) {
            return currentValue * 0.6f;
        }
        return currentValue;
    }
}
