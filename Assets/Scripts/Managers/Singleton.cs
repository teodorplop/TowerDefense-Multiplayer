using UnityEngine;
using System;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T _instance;
	public static T Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<T>();
			}
			if (_instance == null) {
				var go = new GameObject();
				_instance = go.AddComponent<T>();
				DontDestroyOnLoad(_instance);
			}
			return _instance;
		}
	}
	
	protected virtual void Awake() {
		if (_instance == null) {
			_instance = this.GetComponent<T>();
			DontDestroyOnLoad(_instance);
		} else if (_instance != this) {
			DestroyImmediate(this);
		}
	}
}
