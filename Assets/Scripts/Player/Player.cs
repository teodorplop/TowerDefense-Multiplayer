using UnityEngine;

public enum PlayerId { Player1 = 0, Player2 = 1 }

public class Player {
	private PlayerId _id;
	private PlayerPath _path;
	private float _maxHealth;
	private float _health;
	private int _currency;

    public PlayerId ID {
        get { return _id; }
    }
	public int Currency {
		get { return _currency; }
	}
	
	public Player(PlayerId id, float maxHealth, int initialCurrency) {
		_id = id;
		_maxHealth = _health = maxHealth;
		_currency = initialCurrency;
		_path = PlayerServices.GetPlayerPath(_id);
	}

    // For the moment, it is real time, after we can do it as TBS
    public void AddCommand(Command command) {
        command.Execute();
    }
	
	public void ApplyDamage(float value) {
		_health = Mathf.Max(0f, _health - value);
		EventManager.PlayerChangedHP(_id, _health / _maxHealth);
		
		if (_health == 0f) {
			GameManager.Instance.GameOver(_id);
		}
	}
	public void ApplyCost(int cost) {
		_currency = Mathf.Max(0, _currency - cost);
		EventManager.PlayerCurrencyChanged(_id, _currency);
	}
	public Transform GetNextNode(int index) {
		return _path.GetNextNode(index);
	}
}
