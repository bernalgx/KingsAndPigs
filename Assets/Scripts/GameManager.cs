using UnityEditor.Build.Content;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	[SerializeField] private PlayerController _playerController;

	public PlayerController PlayerController { get => _playerController; }

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else Destroy(gameObject);
	}

	public void AddScore()
	{
		Debug.Log("Added Score");
	}
}
