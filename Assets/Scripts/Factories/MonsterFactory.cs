using UnityEngine;
using System.Collections;

public class MonsterFactory : Singleton<MonsterFactory> {
	[SerializeField]
	private Transform _monsterFolder;
	public void Instantiate(PlayerId owner, PlayerId enemy, MonsterType type) {
		GameObject monsterObj = MonoBehaviour.Instantiate(Resources.Load(GetPath(enemy, type))) as GameObject;
		monsterObj.transform.parent = _monsterFolder;
		
		Monster monster = monsterObj.GetComponent<Monster>();
		monster.SummonToPlayer(PlayerManager.Instance.Player(enemy));
	}

	string GetPath(PlayerId player, MonsterType type) {
		string path = "Monsters/" + player.ToString() + "/" + type.ToString();
		return path;
	}
}
