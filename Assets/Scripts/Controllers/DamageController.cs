using UnityEngine;
using System.Collections;

public class DamageController {
    public static float CalculateDamage(float damageValue, TurretType turretType, MonsterType monsterType) {
        foreach (DamageModifier modifier in Modifiers()) {
            if (modifier.IsApplicable(turretType, monsterType)) {
                damageValue = modifier.CalculateDamage(turretType, monsterType, damageValue);
            }
        }

        return damageValue;
    }

    public static DamageModifier[] Modifiers() {
        DamageModifier[] modifiers = { new ElfTowerModifier(), new HumanTowerModifier(), new OrcTowerModifier() };

        return modifiers;
    }
}
