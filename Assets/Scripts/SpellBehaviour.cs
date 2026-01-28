using UnityEngine;

public abstract class SpellBehaviour : MonoBehaviour
{
	protected SpellData data;
	protected Transform caster;

	public virtual void Initialize(SpellData spellData, Transform casterTransform)
	{
		data = spellData;
		caster = casterTransform;
	}
}
