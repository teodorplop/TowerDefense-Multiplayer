using UnityEngine;
using System.Collections.Generic;

public enum TurretType {Piercing, Magic, Siege};

public class Turret : MonoBehaviour {
    public PlayerId playerId;

    [SerializeField]
    public float damageValue;
	[SerializeField]
	private float _delay;
    [SerializeField]
    protected TurretType _type;
    public TurretType Type {
    	get { return _type; }
    }
	private List<Monster> _inRangeMonsters = new List<Monster>();
	protected Monster _target;
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Monster") {
			_inRangeMonsters.Add(collider.gameObject.GetComponent<Monster>());
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "Monster") {
			Monster monster = collider.gameObject.GetComponent<Monster>();
			_inRangeMonsters.Remove(monster);
		}
	}
	
	void SetTarget() {
		if (_target != null && _inRangeMonsters.Contains(_target)) {
			return;
		}
		
		Monster chosenTarget = null;
		float chosenDistance = Mathf.Infinity;

        _inRangeMonsters.RemoveAll(item => item == null);

		foreach (var monster in _inRangeMonsters) {
			float distance = Vector3.Distance(transform.position, monster.transform.position);
			if (distance < chosenDistance) {
				chosenTarget = monster;
				chosenDistance = distance;
			}
		}
		
		_target = chosenTarget;
	}
	
	protected virtual void Attack() {
		EventManager.TurretAttack(_type);
	}
	
	private float _currentDelay = 0f;
	void Update() {
		_currentDelay = Mathf.Max(0f, _currentDelay - Time.deltaTime);
		
		if (_currentDelay <= 0f) {
			SetTarget();
			if (_target != null) {
				_currentDelay = _delay;
				Attack();
			}
		}
	}
}
