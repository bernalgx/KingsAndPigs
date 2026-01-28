using UnityEngine;

public class RunState : PlayerState
{
	public RunState(Player player) : base(player) { }





	public override void Enter()
	{
		player.animator.Play("Run");
	}

	public override void Update()
	{
		if (player.input.Move == 0)
			player.ChangeState(player.idleState);

		if (player.input.JumpPressed)
			player.ChangeState(player.jumpState);
	}

	public override void FixedUpdate()
	{
		player.rb.linearVelocity = new Vector2(
			player.input.Move * player.MoveSpeed,
			player.rb.linearVelocity.y
		);
	}

}
