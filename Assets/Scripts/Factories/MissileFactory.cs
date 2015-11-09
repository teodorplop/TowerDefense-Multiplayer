using UnityEngine;
using System.Collections;

public class MissileFactory : Singleton<MissileFactory> {
    private Transform _missiles;

    new void Awake() {
        _missiles = new GameObject("Missiles").transform;
        _missiles.transform.parent = this.transform;
    }

    public Missile GetMissile(PlayerId id, TurretType type) {
        string path = "Missiles/" + type.ToString() + "Missile";
        GameObject prototype = Resources.Load(path) as GameObject;
        GameObject go = GameObject.Instantiate(prototype);

        go.transform.parent = this.transform;

        return go.GetComponent<Missile>();
    }
}
