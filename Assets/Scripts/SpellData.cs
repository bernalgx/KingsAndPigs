using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "Scriptable Objects/SpellData")]

public class SpellData : ScriptableObject
{
	public string id;
	public string displayName;

	public float damage;
	public float cooldown;
	public float duration;
	public float speed;

	public SpellSpawnDirection spawnDirection;

	public GameObject prefab;
}

