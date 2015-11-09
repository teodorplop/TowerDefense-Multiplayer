using UnityEngine;
using UnityEngine.UI;

public class TurretSpotFactory : Singleton<TurretSpotFactory> {
	[SerializeField]
	private Transform _turretSpotFolder;
	[SerializeField]
	private RectTransform _canvas;
	
	public TurretSpotUI Instantiate(PlayerId player, bool type, Vector3 worldPosition) {
		GameObject turretSpotObj = MonoBehaviour.Instantiate(Resources.Load(GetPath(player, type))) as GameObject;
		turretSpotObj.transform.SetParent(_turretSpotFolder, false);
		
		Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
		Vector2 canvasPosition = new Vector3(viewportPosition.x * _canvas.sizeDelta.x - _canvas.sizeDelta.x * .5f,
											 viewportPosition.y * _canvas.sizeDelta.y - _canvas.sizeDelta.y * .5f);
		
		turretSpotObj.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
		
		return turretSpotObj.GetComponent<TurretSpotUI>();
	}
	
	string GetPath(PlayerId player, bool type) {
		return "UI/" + player.ToString() + (type ? "/TurretSpotSpawn" : "/TurretSpotDestroy");
	}
}
