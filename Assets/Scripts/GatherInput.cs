using UnityEngine;
using UnityEngine.InputSystem;


public class GatherInput : MonoBehaviour
{
	private Controls controls;
	[SerializeField] private Vector2 _value;

	public Vector2 Value { get => _value; }

	[SerializeField]
	private bool _isJumping;
	[SerializeField] private bool _isJumpHeld;
	public bool IsJumping { get => _isJumping; set => _isJumping = value; }
	public bool IsJumpHeld => _isJumpHeld;

	[SerializeField] private bool _isAttacking;
	public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

	[SerializeField] private bool _isDashing;
	public bool IsDashing { get => _isDashing; set => _isDashing = value; }

	[SerializeField] private bool _isSpelling;
	public bool IsSpelling { get => _isSpelling; set => _isSpelling = value; }






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
		controls.Player.Attack.performed += StartAttack;
		controls.Player.Attack.canceled += StopAttack;
		controls.Player.Dash.performed += StartDash;
		controls.Player.Dash.canceled += StopDash;
		controls.Player.Spell.performed += StartSpell;
		controls.Player.Spell.canceled += StopSpell;




		controls.Player.Enable();
	}

	private void StartMove(InputAction.CallbackContext context)
	{
		// Vector2 rawValue = context.ReadValue<Vector2>();
		// Mathf.Sign devuelve 1 si el valor es positivo, -1 si es negativo y 0 si es cero.
		//_value = Mathf.Round(Mathf.Sign(rawValue));
		_value = context.ReadValue<Vector2>().normalized;

		// Si quieres asegurarte de que nunca sea 0 en esta función, puedes usar:
		// _valueX = rawValue > 0 ? 1 : -1; 
	}

	private void StartSpell(InputAction.CallbackContext context)
	{
		_isSpelling = true;
	}

	private void StopSpell(InputAction.CallbackContext context)
	{
		// igual que attack/dash: lo consume el PlayerController
	}


	private void StartDash(InputAction.CallbackContext context)
	{
		_isDashing = true;
	}

	private void StopDash(InputAction.CallbackContext context)
	{
		// no lo apagamos aquí, lo consume PlayerController
	}


	private void StopMove(InputAction.CallbackContext context)
	{
		_value = Vector2.zero;
	}


	private void StartJump(InputAction.CallbackContext context)
	{
		_isJumping = true;
		_isJumpHeld = true;  // 👈 nuevo (held)
	}

	private void StopJump(InputAction.CallbackContext context)
	{
		//_isJumping = false;
		_isJumpHeld = false; // 👈 solo suelta
	}

	private void StartAttack(InputAction.CallbackContext context)
	{
		_isAttacking = true;
	}

	private void StopAttack(InputAction.CallbackContext context)
	{
		// no lo apagamos aquí, lo consume el PlayerController
	}


	private void OnDisable()
	{
		controls.Player.Move.performed -= StartMove;
		controls.Player.Move.canceled -= StopMove;

		controls.Player.Jump.performed -= StartJump;
		controls.Player.Jump.canceled -= StopJump;

		controls.Player.Attack.performed -= StartAttack;
		controls.Player.Attack.canceled -= StopAttack;

		controls.Player.Spell.performed -= StartSpell;
		controls.Player.Spell.canceled -= StopSpell;

		controls.Player.Disable();
	}



}
