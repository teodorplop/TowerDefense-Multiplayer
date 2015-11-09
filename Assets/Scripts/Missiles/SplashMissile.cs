using UnityEngine;
using System.Collections;

public class SplashMissile : Missile {
    [SerializeField] private GameObject explosion;
    [SerializeField] private TurretType turretType;

    private Vector3 _targetPosition;

    public override void Initialize() {
        _targetPosition = targetMonster.transform.position;
		TweenScale.Begin (gameObject, new Vector3 (1f, 1f, 1f), new Vector3 (.5f, .5f, .5f), 2f);
    }

    protected override void Update() {
        Vector3 velocityVector = _targetPosition - this.transform.position;

        if (velocityVector.sqrMagnitude < 0.1f) {
            OnReachedDestination();
            this.Destroy();
            return;
        }

        float angle = Mathf.Atan2(velocityVector.y, velocityVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        transform.position += velocityVector.normalized * Time.deltaTime * speed;
    }

    protected override void OnReachedDestination() {
		GameObject explosionGo = Instantiate(explosion, _targetPosition, Quaternion.identity) as GameObject;
        explosionGo.GetComponent<Explosion>().damageValue = damageValue;
    }
}
