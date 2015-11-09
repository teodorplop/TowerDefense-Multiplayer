using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
    [SerializeField]
	public float speed;
    [HideInInspector]
	public Monster targetMonster;
    [HideInInspector]
    public float damageValue;
    
    protected virtual void Update() {
        if (targetMonster == null) {
            this.Destroy();
            return;
        }
        Vector3 velocityVector = targetMonster.transform.position - this.transform.position;

        if (velocityVector.sqrMagnitude < 0.1f) {
            OnReachedDestination();
            this.Destroy();
            return;
        }

        float angle = Mathf.Atan2(velocityVector.y, velocityVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270f));
        transform.position += velocityVector.normalized * Time.deltaTime * speed;
    }

    protected virtual void Destroy() {
        Destroy(this.gameObject);
    }

    public virtual void Initialize() { }
    protected virtual void OnReachedDestination() { }
}
