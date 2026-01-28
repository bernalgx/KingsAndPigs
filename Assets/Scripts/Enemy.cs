using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int health = 3;

	public bool TakeDamage(int damage)
	{
		health -= damage;
		Debug.Log($"{name} took {damage} damage. HP: {health}");

		if (health <= 0)
		{
			Die();
			return true; // 👈 murió
		}

		return false; // 👈 sigue vivo
	}

	private void Die()
	{
		Debug.Log($"{name} died");
		Destroy(gameObject);
	}
}
