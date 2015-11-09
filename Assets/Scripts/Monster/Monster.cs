using UnityEngine;
using UnityEngine.Networking;

public enum MonsterType {Fighter, ADC, Mage};

public class Monster : NetworkBehaviour {
#region MODEL LIKE INFORMATIONS
    [SerializeField]
    private MonsterType _type;
#endregion    

#region GETTERS SETTERS
    public MonsterType Type {
        get { return _type; }
    }
#endregion

#region MOVEMENT
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _damage;
	private int _targetIndex;
	private Vector3 _targetPosition;
	private Player _player;
	
	public void SummonToPlayer(Player player) {
		_player = player;
		_targetIndex = 0;
		
		SetTarget(_player.GetNextNode(_targetIndex));
		transform.position = _targetPosition;
	}
	void SetTarget(Transform target) {
        _targetPosition = target.position;// +new Vector3(Random.Range(-.3f, .3f), Random.Range(-.3f, .3f));
	}

	void Update() {
		if (Vector3.Distance(transform.position, _targetPosition) < 0.05f) {
			++_targetIndex;
			Transform newTarget = _player.GetNextNode(_targetIndex);
			if (newTarget == null) {
				_player.ApplyDamage(_damage);
				Destroy(gameObject);
				return;
			} else {
				SetTarget(newTarget);
			}
		}
		
		Vector3 direction = (_targetPosition - transform.position).normalized;
		transform.position += direction * _speed * Time.deltaTime;
		
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (new Vector3 (0f, 0f, angle)), 5f * Time.deltaTime);
	}
#endregion

#region DAMAGEABLE
    [SerializeField]
	private float _healthPoints;

    public float HealthPoints {
        get { return _healthPoints; }
    }

    public void ApplyDamage(float value) {
        _healthPoints -= value;

        if (_healthPoints < 0) {
            _healthPoints = 0;

            Die();
        }
    }

    public void Die() {
    	_player.ApplyCost(-CurrencyController.CoinsReceived(_type));
    	EventManager.MonsterDied();
		Destroy (gameObject);
    }
#endregion
}
