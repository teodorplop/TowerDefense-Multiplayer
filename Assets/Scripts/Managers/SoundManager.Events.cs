using UnityEngine;
using System.Collections;

public partial class SoundManager {
	void OnEnable() {
		EventManager.MonsterDiedEvent += MonsterDiedHandler;
		EventManager.TurretAttackEvent += TurretAttackHandler;
	}
	void OnDisable() {
		EventManager.MonsterDiedEvent -= MonsterDiedHandler;
		EventManager.TurretAttackEvent -= TurretAttackHandler;
	}
	
	void MonsterDiedHandler() {
		PlayClip(GetClip(SoundType.Death));
	}
	void TurretAttackHandler(TurretType type) {
		AudioClip clip = null;
		switch (type) {
		case TurretType.Magic:
			clip = GetClip(SoundType.Magic);
			break;
		case TurretType.Piercing:
			clip = GetClip(SoundType.Piercing);
			break;
		case TurretType.Siege:
			clip = GetClip(SoundType.Siege);
			break;
		default:
			clip = null;
			break;
		}
		
		PlayClip(clip);
	}
}
