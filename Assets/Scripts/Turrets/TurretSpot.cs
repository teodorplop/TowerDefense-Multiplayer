using UnityEngine;
using System.Collections;

public class TurretSpot : MonoBehaviour {
	[HideInInspector]
	public PlayerId owner;
	
	public void Clicked() {
		if (_uiSpawned == null) {
			TurretSpotManager.Instance.SpotSelected(owner, this);
		} else {
			TurretSpotManager.Instance.SpotDeselected(owner, this);
		}
	}
	
	private TurretSpotUI _uiSpawned;
	public void Select() {
		_uiSpawned = TurretSpotFactory.Instance.Instantiate(owner, transform.childCount == 0, transform.position);
		_uiSpawned.SetSpot(this);
		TweenScale.Begin(_uiSpawned.gameObject, Vector3.zero, new Vector3(1f, 1f, 1f), .2f);
	}
	public void Deselect() {
		StartCoroutine(DeselectAnimation());
	}
	
	IEnumerator DeselectAnimation() {
		yield return new WaitForSeconds(.05f);
		TweenScale.Begin(_uiSpawned.gameObject, new Vector3(1f, 1f, 1f), Vector3.zero, .2f);
		Destroy(_uiSpawned.gameObject, .25f);
	}
}
