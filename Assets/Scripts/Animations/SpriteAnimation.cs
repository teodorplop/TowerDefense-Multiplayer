using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
	public static void Begin(GameObject target, float duration, Sprite[] sprites) {
		var spriteAnimation = target.GetComponent<SpriteAnimation>();
		if (spriteAnimation == null) {
			spriteAnimation = target.AddComponent<SpriteAnimation>();
		}
		spriteAnimation.Begin(duration, sprites);
	}
	public static bool IsPlaying(GameObject target) {
		return target.GetComponent<SpriteAnimation>() != null;
	}

	private SpriteRenderer _renderer;
	private Sprite[] _sprites;
	private int _index;
	private float _frameDuration;
	private float _timeSinceLastFrame;

	void Begin(float duration, Sprite[] sprites) {
		this._sprites = sprites;
		this._index = 0;
		this._frameDuration = (sprites.Length > 1) ? (duration / sprites.Length) : Mathf.Infinity;
		this._timeSinceLastFrame = 0f;

		_renderer = GetComponent<SpriteRenderer>();
		_renderer.sprite = sprites[0];
	}

	void Update() {
		_timeSinceLastFrame += Time.deltaTime;
		if (_timeSinceLastFrame >= _frameDuration) {
			_timeSinceLastFrame -= _frameDuration;
			++_index;

			if (_index >= _sprites.Length) {
				Destroy (this);
			} else {
				_renderer.sprite = _sprites[_index];
			}
		}
	}
}
