using UnityEngine;
using System.Collections;

public class TurretDestroyCommand : Command {
	private PlayerId _player;
	private int _spot;
	
	public TurretDestroyCommand(PlayerId player, int spot) {
		_player = player;
		_spot = spot;
	}
	
	public override void Execute() {
		PlayerManager.Instance.TurretDestroyed(_player, _spot);
	}
}
