using UnityEngine;
using System.Linq;

public class PlayerServices {
	public static PlayerPath GetPlayerPath(PlayerId id) {
		PlayerPath[] playerPaths = MonoBehaviour.FindObjectsOfType<PlayerPath>();
		return playerPaths.FirstOrDefault(obj => obj.playerID == id);
	}
	public static PlayerUI GetPlayerUI(PlayerId id) {
		PlayerUI[] playerUIs = MonoBehaviour.FindObjectsOfType<PlayerUI>();
		return playerUIs.FirstOrDefault(obj => obj.playerID == id);
	}
}
