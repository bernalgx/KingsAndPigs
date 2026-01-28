using UnityEngine;

public abstract class SpellBase : MonoBehaviour, ISpell
{
	protected SpellContext context;
	protected float lastCastTime;

	public virtual void Initialize(SpellContext spellContext)
	{
		context = spellContext;
	}

	public virtual bool CanCast()
	{
		return Time.time >= lastCastTime + context.data.cooldown;
	}

	public virtual void Cast()
	{
		lastCastTime = Time.time;
	}
}
