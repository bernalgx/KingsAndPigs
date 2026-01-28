using UnityEngine;

public interface ISpell
{
	/// <summary>
	/// Inicializa el spell con sus datos y el caster.
	/// Se llama UNA sola vez al crearse.
	/// </summary>
	void Initialize(SpellContext context);

	/// <summary>
	/// Ejecuta el spell (cast).
	/// Puede ser instantáneo o iniciar comportamiento persistente.
	/// </summary>
	void Cast();

	/// <summary>
	/// Devuelve true si el spell puede ejecutarse ahora (cooldown, recursos, estado).
	/// </summary>
	bool CanCast();
}
