using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TNetworkManager : NetworkBehaviour {

    void Awake() {
        SNetworkManager.Instance.AssignTManager(this);
    }

    void Start() {
        if (isServer) {
            PlayerManager.Instance.ActivatePlayer(PlayerId.Player1);
        } else {
            PlayerManager.Instance.ActivatePlayer(PlayerId.Player2);
        }
    }

    public void RPCCommandSpawnTurret(TurretType type, int turretSpot) {
        if (!base.isServer) {
            CmdInstantiateTurretToServer(type, turretSpot);
        } else {
            RpcInstantiateTurretToClient(type, turretSpot);
        }
    }

    public void RPCCommandSpawnMonster(MonsterType type) {
        if (!base.isServer) {
            CmdInstantiateMonsterToServer(type);
        } else {
            RpcInstantiateMonsterToClient(type);
        }
    }

    public void RPCCommandDestroyTurret(int turretSpot) {
        if (!base.isServer) {
            CmdDestroyTurretToServer(turretSpot);
        } else {
            RpcDestroyTurretToClient(turretSpot);
        }
    }

    [Command]
    void CmdInstantiateMonsterToServer(MonsterType monsterType) {
        if (!base.isServer) {
            return;
        }
        MonsterSpawnCommand monsterSpawnCommand = new MonsterSpawnCommand(PlayerManager.Instance.NonActivePlayer.ID, PlayerManager.Instance.ActivePlayer.ID, monsterType);
        PlayerManager.Instance.NonActivePlayer.AddCommand(monsterSpawnCommand);
    }   

    [ClientRpc]
    void RpcInstantiateMonsterToClient(MonsterType monsterType) {
        if (base.isServer) {
            return;
        }
        MonsterSpawnCommand monsterSpawnCommand = new MonsterSpawnCommand(PlayerManager.Instance.NonActivePlayer.ID, PlayerManager.Instance.ActivePlayer.ID, monsterType);
        PlayerManager.Instance.NonActivePlayer.AddCommand(monsterSpawnCommand);
    }

    [Command]
    void CmdInstantiateTurretToServer(TurretType turretType, int turretSpot) {
        if (!base.isServer) {
            return;
        }
        TurretSpawnCommand turretSpawnCommand = new TurretSpawnCommand(PlayerManager.Instance.NonActivePlayer.ID, turretType, turretSpot);
        PlayerManager.Instance.NonActivePlayer.AddCommand(turretSpawnCommand);
    }

    [ClientRpc]
    void RpcInstantiateTurretToClient(TurretType turretType, int turretSpot) {
        if (base.isServer) {
            return;
        }
        TurretSpawnCommand turretSpawnCommand = new TurretSpawnCommand(PlayerManager.Instance.NonActivePlayer.ID, turretType, turretSpot);
        PlayerManager.Instance.NonActivePlayer.AddCommand(turretSpawnCommand);
    }

    [Command]
    void CmdDestroyTurretToServer(int turretSpot) {
        if (!base.isServer) {
            return;
        }
        TurretDestroyCommand turretSpawnCommand = new TurretDestroyCommand(PlayerManager.Instance.NonActivePlayer.ID, turretSpot);
        PlayerManager.Instance.NonActivePlayer.AddCommand(turretSpawnCommand);
    }

    [ClientRpc]
    void RpcDestroyTurretToClient(int turretSpot) {
        if (base.isServer) {
            return;
        }
        TurretDestroyCommand turretSpawnCommand = new TurretDestroyCommand(PlayerManager.Instance.NonActivePlayer.ID, turretSpot);
        PlayerManager.Instance.NonActivePlayer.AddCommand(turretSpawnCommand);
    }
}
