using UnityEngine;
using UnityEngine.UI;

public class TurretSpawnButton : MonoBehaviour {
	public PlayerId playerId;
	public TurretType type;
	public TurretSpot spot;
	
	private Button _button;
	void Awake() {
		_button = GetComponent<Button>();
	}
	
	public void OnButtonPressed() {
		int spotIndex = TurretServices.TurretSpotToInt(playerId, spot);
		
		Command turretSpawnCommand = new TurretSpawnCommand(playerId, type, spotIndex);
		PlayerManager.Instance.Player(playerId).AddCommand(turretSpawnCommand);

        SNetworkManager.Instance.SpawnTurret(type, spotIndex);
	}
	
	void Update() {
		bool canAfford = (CurrencyController.Price(type) <= PlayerManager.Instance.Player(playerId).Currency);
		_button.interactable = canAfford;
	}
}
