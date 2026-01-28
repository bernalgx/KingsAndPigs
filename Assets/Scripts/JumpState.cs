using UnityEngine;

public class JumpState : PlayerState
{
	public JumpState(Player player) : base(player) { }

	public override void Enter()
	{
		if (player.rb != null)
		{
			player.rb.linearVelocity = new Vector2(
				player.rb.linearVelocity.x, // Mantener velocidad horizontal
				player.jumpForce
			);
		}

		if (player.animator != null)
			player.animator.Play("Jump");
	}

	public override void Update()
	{
		if (player.rb == null) return;

		// Transición a Fall cuando empieza a caer
		if (player.rb.linearVelocity.y < 0)
			player.ChangeState(player.fallState);
	}

	public override void FixedUpdate()
	{
		// Aplicar movimiento horizontal durante el salto (como legacy)
		if (player.rb != null && player.input != null)
		{
			player.rb.linearVelocity = new Vector2(
				player.input.Move * player.MoveSpeed,
				player.rb.linearVelocity.y
			);
		}
	}
}