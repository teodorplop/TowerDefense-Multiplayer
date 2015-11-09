using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public delegate void PlayerChangedHPHandler(PlayerId playerID, float percentage);
	public static event PlayerChangedHPHandler PlayerChangedHPEvent;
	public static void PlayerChangedHP(PlayerId playerID, float percentage) {
		if (PlayerChangedHPEvent != null) {
			PlayerChangedHPEvent(playerID, percentage);
		}
	}
	public delegate void PlayerCurrencyChangedHandler(PlayerId playerID, int currency);
	public static event PlayerCurrencyChangedHandler PlayerCurrencyChangedEvent;
	public static void PlayerCurrencyChanged(PlayerId playerID, int currency) {
		if (PlayerCurrencyChangedEvent != null) {
			PlayerCurrencyChangedEvent(playerID, currency);
		}
	}
	
	public delegate void GameEndedHandler(PlayerId winner);
	public static event GameEndedHandler GameEndedEvent;
	public static void GameEnded(PlayerId winner) {
		if (GameEndedEvent != null) {
			GameEndedEvent(winner);
		}
	}
	
	public delegate void PlayerIsActiveHandler(PlayerId playerID);
	public static event PlayerIsActiveHandler PlayerIsActiveEvent;
	public static void PlayerIsActive(PlayerId playerID) {
		if (PlayerIsActiveEvent != null) {
			PlayerIsActiveEvent(playerID);
		}
	}
	
	public delegate void MonsterDiedHandler();
	public static event MonsterDiedHandler MonsterDiedEvent;
	public static void MonsterDied() {
		if (MonsterDiedEvent != null) {
			MonsterDiedEvent();
		}
	}
	
	public delegate void TurretAttackHandler(TurretType type);
	public static event TurretAttackHandler TurretAttackEvent;
	public static void TurretAttack(TurretType type) {
		if (TurretAttackEvent != null) {
			TurretAttackEvent(type);
		}
	}
}
