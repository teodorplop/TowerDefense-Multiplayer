using UnityEngine;
using System.Linq;

public class TurretServices : MonoBehaviour {
	public static TurretSpot IntToTurretSpot(PlayerId id, int index) {
		TurretHelper[] turretHelpers = FindObjectsOfType<TurretHelper>();
		return turretHelpers.FirstOrDefault(obj => obj.player == id).IntToTurretSpot(index);
	}
	public static int TurretSpotToInt(PlayerId id, TurretSpot spot) {
		TurretHelper[] turretHelpers = FindObjectsOfType<TurretHelper>();
		return turretHelpers.FirstOrDefault(obj => obj.player == id).TurretSpotToInt(spot);
	}
}
