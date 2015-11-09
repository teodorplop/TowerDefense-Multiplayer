using UnityEngine;
using System.Collections;

public class TargetedTurret : Turret {
    protected override void Attack() {
    	base.Attack();
    
        Missile missile = MissileFactory.Instance.GetMissile(playerId, _type);

        missile.transform.position = this.transform.position;
        missile.targetMonster = _target;
        missile.damageValue = damageValue;

        missile.Initialize();
    }
}
