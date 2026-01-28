using UnityEngine;

public class FallState : PlayerState
{
	public FallState(Player player) : base(player) { }

	public override void Update()
	{
		if (player.IsGrounded())
			player.ChangeState(player.idleState);
	}


}

