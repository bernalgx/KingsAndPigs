using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField] private Player _player;
	public Player Player => _player;

	[SerializeField] private int _diamondCollected;
	public int DiamondCollected => _diamondCollected;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(gameObject);
	}

	public void AddDiamond()
	{
		_diamondCollected++;
	}
}
