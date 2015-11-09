using UnityEngine;
using System.Collections.Generic;

public class TurretSpotManager : Singleton<TurretSpotManager> {
	private Dictionary<PlayerId, TurretSpot> _selected = new Dictionary<PlayerId, TurretSpot>();
	
	public void SpotSelected(PlayerId playerID, TurretSpot spot) {
		if (!_selected.ContainsKey(playerID)) {
			_selected.Add(playerID, spot);
		} else {
			if (_selected[playerID] != null) {
				_selected[playerID].Deselect();
			}
			_selected[playerID] = spot;
		}
		
		spot.Select();
	}
	public void SpotDeselected(PlayerId playerID, TurretSpot spot) {
		if (_selected.ContainsKey(playerID) && _selected[playerID] != null) {
			_selected[playerID].Deselect();
			_selected[playerID] = null;
		}
	}
}
