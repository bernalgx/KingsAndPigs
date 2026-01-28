using UnityEngine;

public class MacheteHitbox : MonoBehaviour
{
	public bool active;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!active) return;

		Debug.Log($"Machete hit: {other.name}");
	}
}
