using UnityEngine;

public class TestKnock : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			// Changed from PlayerController to Player
			Player player = collision.GetComponent<Player>();
			if (player != null)
			{
				player.Knockback();
			}

			// Changed from PlayerController to Player
			if (GameManager.instance != null && GameManager.instance.Player != null)
			{
				Debug.Log(GameManager.instance.Player.name);
			}
		}
	}
}