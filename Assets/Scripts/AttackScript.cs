using UnityEngine;

public class AttackState : PlayerState
{
	public AttackState(Player player) : base(player) { }

	public override void Enter()
	{
		player.animator.Play("Attack");
	}

	public override void Update()
	{
		if (player.animator.IsFinished())
			player.ChangeState(player.idleState);
	}
}
