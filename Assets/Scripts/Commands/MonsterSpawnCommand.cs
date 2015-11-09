using UnityEngine;
using System.Collections;

public class MonsterSpawnCommand : Command {
    private PlayerId _owner;
    private PlayerId _enemy;
    private MonsterType _type;

    public MonsterSpawnCommand(PlayerId owner, PlayerId enemy, MonsterType type) {
        _type = type;
        _owner = owner;
        _enemy = enemy;
    }

    public override void Execute() {
        MonsterFactory.Instance.Instantiate(_owner, _enemy, _type);
        PlayerManager.Instance.Player(_owner).ApplyCost(CurrencyController.Price(_type));
    }
}
