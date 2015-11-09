using UnityEngine;
using System.Collections;

public class PlayerManager : Singleton<PlayerManager> {
	[SerializeField]
	private float _playerMaxHP;
	[SerializeField]
	private int _playerInitialCurrency;
	private Player[] _players;

    private Player _activePlayer;

	new void Awake() {
		_players = new Player[2];

        for(int i = 0; i < 2; i++) {
			_players[i] = new Player((PlayerId)i, _playerMaxHP, _playerInitialCurrency);
        }
	}
	public void TurretDestroyed(PlayerId player, int spotIndex) {
		TurretSpot spot = TurretServices.IntToTurretSpot(player, spotIndex);
	
		Transform turretTransform = spot.transform.GetChild(0);
		if (turretTransform) {
			Turret turret = turretTransform.GetComponent<Turret>();
			TurretType type = turret.Type;
			
			Player(player).ApplyCost(-CurrencyController.CoinsReceived(type));
			Destroy(turret.gameObject);
		}
	}

    public Player Player(PlayerId id) {
        return _players[(int)id];
    }

    public Player ActivePlayer {
        get { return _activePlayer; }
    }

    public Player NonActivePlayer {
        get { return _activePlayer == _players[0] ? _players[1] : _players[0]; }
    }

    public void ActivatePlayer(PlayerId playerId) {
        _activePlayer = _players[(int)playerId];
        EventManager.PlayerIsActive(playerId);
    }
}
