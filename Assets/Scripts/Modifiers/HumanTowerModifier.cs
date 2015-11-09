using UnityEngine;
using System.Collections;

public class HumanTowerModifier : DamageModifier {

    public override bool IsApplicable(TurretType turretType, MonsterType monsterType) {
        return turretType == TurretType.Magic;
    }

    public override float CalculateDamage(TurretType turretType, MonsterType monsterType, float currentValue) {
        if (monsterType == MonsterType.Fighter) {
            return currentValue * 0.3f;
        }
        if (monsterType == MonsterType.ADC) {
            return currentValue;
        }
        return currentValue * 0.6f;
    }
}
