using UnityEngine;
using System.Collections;

public class TurretFactory : Singleton<TurretFactory> {
	public void Instantiate(PlayerId player, TurretType type, TurretSpot spot) {
		GameObject turretObj = MonoBehaviour.Instantiate(Resources.Load(GetPath(player, type))) as GameObject;
		turretObj.transform.parent = spot.transform;
		turretObj.transform.localPosition = new Vector3(0f, .5f, 0f);
	}
	
	string GetPath(PlayerId player, TurretType type) {
		string path = "Turrets/" + player.ToString() + "/" + type.ToString();
		return path;
	}
}
