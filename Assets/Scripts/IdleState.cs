using UnityEngine;

public class IdleState : PlayerState
{
	public IdleState(Player player) : base(player) { }

	public override void Enter()
	{
		if (player.animator != null)
			player.animator.Play("Idle");
	}

	public override void Update()
	{
		if (player.input == null) return;

		if (player.input.Move != 0)
			player.ChangeState(player.runState);

		if (player.input.JumpPressed)
			player.ChangeState(player.jumpState);

		if (player.input.AttackPressed)
			player.ChangeState(player.attackState);

		if (player.input.CastPressed)
			player.ChangeState(player.castState);
	}

	public override void FixedUpdate()
	{
		// Mantener velocidad horizontal en 0 (como legacy)
		if (player.rb != null)
		{
			player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
		}
	}
}