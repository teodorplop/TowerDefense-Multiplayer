using UnityEngine;

public class DevUtils : Singleton<DevUtils> {
	public bool devMode = true;
	public static bool DevMode {
		get { return Instance.devMode; }
	}
}
