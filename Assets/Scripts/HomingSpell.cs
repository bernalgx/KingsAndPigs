using UnityEngine;
public class HomingSpell : SpellBase
{
	private Transform target;

	public override void Cast()
	{
		base.Cast();
		// lógica de búsqueda de target
	}

	void Update()
	{
		if (target == null) return;

		Vector2 dir = (target.position - transform.position).normalized;
		transform.position += (Vector3)(dir * context.data.speed * Time.deltaTime);
	}
}

