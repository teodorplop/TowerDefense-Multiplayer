using UnityEngine;
using System.Collections;

public class SNetworkManager : Singleton<SNetworkManager> {
    [SerializeField]
    private TNetworkManager _realNetworkManager;

    public void AssignTManager(TNetworkManager realNetworkManager) {
        _realNetworkManager = realNetworkManager;
    }

    public void SpawnMonster(MonsterType monsterType) {
        _realNetworkManager.RPCCommandSpawnMonster(monsterType);
    }

    public void SpawnTurret(TurretType turretType, int turretSpot) {
        _realNetworkManager.RPCCommandSpawnTurret(turretType, turretSpot);
    }

    public void DestroyTurret(int turretSpot) {
        _realNetworkManager.RPCCommandDestroyTurret(turretSpot);
    }
}
