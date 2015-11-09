using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
	public PlayerId playerID;
	
	void Start() {
		_currencyText.text = PlayerManager.Instance.Player(playerID).Currency.ToString();
	}
	
	void OnEnable() {
		EventManager.PlayerChangedHPEvent += PlayerChangedHP;
		EventManager.PlayerCurrencyChangedEvent += PlayerCurrencyChanged;
		EventManager.PlayerIsActiveEvent += PlayerIsActive;
		EventManager.GameEndedEvent += GameEnded;
	}

	void OnDisable() {
		EventManager.PlayerChangedHPEvent -= PlayerChangedHP;
		EventManager.PlayerCurrencyChangedEvent -= PlayerCurrencyChanged;
		EventManager.PlayerIsActiveEvent -= PlayerIsActive;
		EventManager.GameEndedEvent -= GameEnded;
	}
	
	[SerializeField]
	private Image _hpImage;
	void PlayerChangedHP(PlayerId id, float percentage) {
		if (playerID == id) {
			_hpImage.fillAmount = percentage;
		}
	}
	[SerializeField]
	private Text _currencyText;
	void PlayerCurrencyChanged(PlayerId id, int currency) {
		if (playerID == id) {
			_currencyText.text = currency.ToString();
		}
	}
	
	[SerializeField]
	private Image _overlay;
	void PlayerIsActive(PlayerId playerID) {
		if (DevUtils.DevMode) {
			_overlay.enabled = false;
			return;
		}

		_overlay.enabled = (this.playerID != playerID);
	}
	
	[SerializeField]
	private Text _endText;
	void GameEnded(PlayerId winner) {
		_overlay.enabled = true;
		if (PlayerManager.Instance.ActivePlayer.ID == playerID) {
			_endText.text = (playerID == winner) ? "You WON!" : "You LOSE!";
			_endText.gameObject.SetActive(true);
		}
	}
}
