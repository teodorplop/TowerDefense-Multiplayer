using UnityEngine;
using System.Collections;

public class TurretDestroyButton : MonoBehaviour {
	public PlayerId playerId;
	public TurretSpot spot;
	
	public void OnButtonPressed() {
		int spotIndex = TurretServices.TurretSpotToInt(playerId, spot);
		Command turretDestroyCommand = new TurretDestroyCommand(playerId, spotIndex);
		PlayerManager.Instance.Player(playerId).AddCommand(turretDestroyCommand);

        SNetworkManager.Instance.DestroyTurret(spotIndex);
	}
}
