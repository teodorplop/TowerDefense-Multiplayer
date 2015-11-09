using UnityEngine;

public class Scanner {
	public static T ScanFor<T>(Vector3 mousePosition, LayerMask mask) where T : MonoBehaviour {
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay(mousePosition);
		if (!Physics.Raycast(ray, out hit, 1000, mask)) {
			return null;
		}
		
		var clickedObject = hit.collider.gameObject;
		return clickedObject.GetComponent<T>();
	}
}
