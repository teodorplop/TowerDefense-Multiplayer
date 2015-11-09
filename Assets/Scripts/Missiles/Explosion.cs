using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    public float damageValue;

	[SerializeField]
	private Sprite[] _animationSprites;
    [SerializeField]
	private float _duration;
	[SerializeField]
	private float _radius;
    private float _timer = 0f;

	void Start() {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);
		foreach (Collider2D collider in colliders) {
			if (collider.gameObject.tag == "Monster") {
				collider.GetComponent<Monster>().ApplyDamage(damageValue);
			}
		}

		SpriteAnimation.Begin(gameObject, .5f, _animationSprites);
	}

    void Update() {
        _timer += Time.deltaTime;

        if (_timer > _duration) {
            Destroy(this.gameObject);
            return;
        }
    }
}
