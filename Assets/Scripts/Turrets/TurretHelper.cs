using UnityEngine;
using System.Collections.Generic;

public class TurretHelper : MonoBehaviour {
	public PlayerId player;
	
	private Dictionary<int, TurretSpot> _intToTurretSpot;
	private Dictionary<TurretSpot, int> _turretSpotToInt;
	void Start() {
		_intToTurretSpot = new Dictionary<int, TurretSpot>();
		_turretSpotToInt = new Dictionary<TurretSpot, int>();
		
		int index = 0;
		foreach (TurretSpot spot in GetComponentsInChildren<TurretSpot>()) {
			_intToTurretSpot.Add(index, spot);
			_turretSpotToInt.Add(spot, index);
			spot.owner = player;
			++index;
		}
	}
	
	public TurretSpot IntToTurretSpot(int index) {
		TurretSpot ans = null;
		_intToTurretSpot.TryGetValue(index, out ans);
		if (ans == null) {
			Debug.LogError("Something's wrong.");
		}
		return ans;
	}
	public int TurretSpotToInt(TurretSpot spot) {
		int ans = -1;
		_turretSpotToInt.TryGetValue(spot, out ans);
		if (ans == -1) {
			Debug.LogError("Something's wrong.");
		}
		return ans;
	}
}
