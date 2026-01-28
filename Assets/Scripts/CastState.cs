using UnityEngine;

public class CastState : PlayerState
{
	public CastState(Player player) : base(player) { }

	public override void Enter()
	{
		player.animator.Play("Cast");
		player.spellController.CastCurrentSpell();
	}



	public override void Update()
	{
		// Cuando termina la animación o frame clave
		if (player.animator.IsFinished())
			player.ChangeState(player.idleState);

	}


}
