using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawnButton : MonoBehaviour {
    public PlayerId owner;
    public PlayerId enemy;
    public MonsterType type;
	public float cooldown;
	private float _currentCooldown;
	private float _cost;

	private Image _cooldown;
	private Button _button;
	void Awake() {
		_cooldown = transform.FindChild ("Cooldown").GetComponent<Image> ();
		_button = GetComponent<Button> ();
	}
	
    public void OnButtonPressed() {
		if (cooldown > 0f) {
			_button.interactable = false;
			_currentCooldown = cooldown;
		}

        Command monsterSpawnCommand = new MonsterSpawnCommand(owner, enemy, type);
        PlayerManager.Instance.Player(owner).AddCommand(monsterSpawnCommand);
        SNetworkManager.Instance.SpawnMonster(type);
    }

	void Update() {
		if (cooldown == 0f) {
			_cooldown.fillAmount = 0f;
		} else {
			_currentCooldown = Mathf.Max (_currentCooldown - Time.deltaTime, 0f);
			_cooldown.fillAmount = _currentCooldown / cooldown;
		}
		
		bool canAfford = (CurrencyController.Price(type) <= PlayerManager.Instance.Player(owner).Currency) && _currentCooldown == 0f;
		_button.interactable = canAfford;
	}
}
