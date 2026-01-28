using UnityEngine;

public class BeamSpell : SpellBase
{
	public override void Cast()
	{
		base.Cast();

		transform.position = context.spawnPoint.position;
		transform.right = -context.caster.right;

		Destroy(gameObject, context.data.duration);
	}
}
