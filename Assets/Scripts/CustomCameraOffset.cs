using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CustomCameraOffset : MonoBehaviour
{
	public CinemachineCamera CinemachineCamera;
	public CinemachinePositionComposer PositionComposer;

	private void Start()
	{
		PositionComposer = CinemachineCamera.GetComponent<CinemachinePositionComposer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		Debug.Log("This is a custom camera offset script.");
		//PositionComposer.TargetOffset.y = -2.5f;
	}
}
