using UnityEngine;
using System.Collections;

public class TurretSpotUI : MonoBehaviour {
	private TurretSpot _spot;
	public void SetSpot(TurretSpot spot) {
		_spot = spot;
	}
	void Start() {
		foreach (var button in GetComponentsInChildren<TurretSpawnButton>()) {
			button.spot = _spot;
		}
		foreach (var button in GetComponentsInChildren<TurretDestroyButton>()) {
			button.spot = _spot;
		}
	}
}
