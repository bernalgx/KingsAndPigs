using UnityEngine;

public static class AnimatorExtensions
{
	public static bool IsFinished(this Animator animator, int layer = 0)
	{
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);
		return info.normalizedTime >= 1f && !animator.IsInTransition(layer);
	}
}
