using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager> {
	public bool inputEnabled = true;
	void Update() {
		if (!inputEnabled) {
			return;
		}
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePosition = Input.mousePosition;
			
			// scan for turret spot
			TurretSpot turretSpot = Scanner.ScanFor<TurretSpot>(mousePosition, 1 << 8);
			if (turretSpot) {
				Player player = PlayerManager.Instance.Player(turretSpot.owner);
				if (DevUtils.DevMode || player == PlayerManager.Instance.ActivePlayer) {
					turretSpot.Clicked();
				}
			}
		}
	}
}
