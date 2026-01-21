using UnityEngine;
using UnityEngine.InputSystem;


public class GatherInput : MonoBehaviour
{
	private Controls controls;
	[SerializeField] private float _valueX;

	public float ValueX { get => _valueX; }

	[SerializeField]
	private bool _isJumping;
	public bool IsJumping { get => _isJumping; set => _isJumping = value; }


	private void Awake()
	{
		controls = new Controls();
	}

	private void OnEnable()
	{
		controls.Player.Move.performed += StartMove;
		controls.Player.Move.canceled += StopMove;
		controls.Player.Jump.performed += StartJump;
		controls.Player.Jump.canceled += StopJump;
		controls.Player.Enable();
	}

	private void StartMove(InputAction.CallbackContext context)
	{
		float rawValue = context.ReadValue<float>();
		// Mathf.Sign devuelve 1 si el valor es positivo, -1 si es negativo y 0 si es cero.
		_valueX = Mathf.RoundToInt(Mathf.Sign(rawValue));

		// Si quieres asegurarte de que nunca sea 0 en esta función, puedes usar:
		// _valueX = rawValue > 0 ? 1 : -1; 
	}

	private void StopMove(InputAction.CallbackContext context)
	{
		_valueX = 0f;
	}


	private void StartJump(InputAction.CallbackContext context)
	{
		_isJumping = true;
	}

	private void StopJump(InputAction.CallbackContext context)
	{
		_isJumping = false;
	}

	private void OnDisable()
	{
		controls.Player.Move.performed -= StartMove;
		controls.Player.Move.canceled -= StopMove;
		controls.Player.Jump.performed -= StartJump;
		controls.Player.Jump.canceled -= StopJump;
		controls.Player.Disable();
	}

}
