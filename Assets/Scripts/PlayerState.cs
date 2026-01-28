using UnityEngine;

// Renombrado para evitar conflictos con Unity
public abstract class PlayerStateBase
{
	protected Player player;

	protected PlayerStateBase(Player player)
	{
		this.player = player;
	}

	public virtual void Enter() { }
	public virtual void Update() { }
	public virtual void FixedUpdate() { }
	public virtual void Exit() { }
}