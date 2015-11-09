using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	public void GameOver(PlayerId loserID) {
		Player activePlayer = PlayerManager.Instance.ActivePlayer;
		Player nonActivePlayer = PlayerManager.Instance.NonActivePlayer;
		
		InputManager.Instance.inputEnabled = false;
		EventManager.GameEnded(activePlayer.ID != loserID ? activePlayer.ID : nonActivePlayer.ID);
	}
}
