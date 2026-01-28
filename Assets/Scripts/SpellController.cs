using UnityEngine;

public class SpellController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Transform forwardSpawn;
	[SerializeField] private Transform backwardSpawn;

	private SpellData currentSpell;
	private float lastCastTime;

	public void CastCurrentSpell()
	{
		if (currentSpell == null) return;

		if (Time.time < lastCastTime + currentSpell.cooldown)
			return;

		Transform spawnPoint =
			currentSpell.spawnDirection == SpellSpawnDirection.Forward
			? forwardSpawn
			: backwardSpawn;

		SpellBehaviour spell =
	Instantiate(currentSpell.prefab, spawnPoint.position, Quaternion.identity)
	.GetComponent<SpellBehaviour>();


		spell.Initialize(currentSpell, player);

		lastCastTime = Time.time;
	}



	public void SetSpell(SpellData spell)
	{
		currentSpell = spell;
	}


}
