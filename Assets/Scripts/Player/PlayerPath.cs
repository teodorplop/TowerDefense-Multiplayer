using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerPath : MonoBehaviour {
	public PlayerId playerID;
	private Transform[] _path;
	
	void Awake() {
		List<Transform> children = new List<Transform>();
		foreach (Transform child in transform) {
			children.Add(child);
		}
		_path = children.ToArray();
	}
	
	public Transform GetNextNode(int index) {
		if (_path == null) {
			Debug.LogError("Path is null.");
			return null;
		}
		if (index >= _path.Length) {
			return null;
		}
		return _path[index];
	}
}
