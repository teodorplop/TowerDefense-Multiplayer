using UnityEngine;
using System.Collections;

public class TurretSpawnCommand : Command {
	private PlayerId _player;
	private TurretType _type;
	private int _spot;
	
	public TurretSpawnCommand(PlayerId player, TurretType type, int spot) {
		_type = type;
		_player = player;
		_spot = spot;
	}
	
	public override void Execute() {
		TurretSpot spot = TurretServices.IntToTurretSpot(_player, _spot);
		TurretFactory.Instance.Instantiate(_player, _type, spot);
        PlayerManager.Instance.Player(_player).ApplyCost(CurrencyController.Price(_type));
	}
}
