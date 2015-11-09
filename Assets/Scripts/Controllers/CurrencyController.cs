using UnityEngine;
using System.Collections;

public class CurrencyController : MonoBehaviour {
	public static int Price(MonsterType type) {
		return 2;
	}
	public static int Price(TurretType type) {
		return 10;
	}
	public static int CoinsReceived(MonsterType type) {
		return 2;
	}
	public static int CoinsReceived(TurretType type) {
		return 10;
	}
}
