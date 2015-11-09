using UnityEngine;
using System.Collections;

public class OrcTowerModifier : DamageModifier  {

    public override bool IsApplicable(TurretType turretType, MonsterType monsterType) {
        return turretType == TurretType.Siege;
    }

    public override float CalculateDamage(TurretType turretType, MonsterType monsterType, float currentValue) {
        if (monsterType == MonsterType.Fighter) {
            return currentValue * 0.6f;
        }
        if (monsterType == MonsterType.ADC) {
            return currentValue * 0.3f;
        }
        return currentValue;
    }
}
