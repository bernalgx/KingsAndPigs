using UnityEngine;

public class Player : MonoBehaviour
{
	private PlayerState currentState;

	[Header("Movement")]
	public float jumpForce = 12f;
	public float MoveSpeed = 5f;

	[Header("Knockback Settings")]
	[SerializeField] private Vector2 knockbackPower = new Vector2(10f, 5f);
	[SerializeField] private float knockedDuration = 0.5f;

	[Header("Gameplay Systems")]
	public SpellController spellController;

	[Header("Ground Check")]
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundCheckRadius = 0.2f;
	[SerializeField] private LayerMask groundLayer;

	// Estados (pueden instanciarse una vez)
	public IdleState idleState;
	public RunState runState;
	public JumpState jumpState;
	public FallState fallState;
	public AttackState attackState;
	public CastState castState;

	[Header("Core Components")]
	public Rigidbody2D rb;
	public Animator animator;
	public GatherInput input;

	void Awake()
	{
		// Validar componentes críticos
		if (rb == null)
			rb = GetComponent<Rigidbody2D>();

		if (animator == null)
			animator = GetComponent<Animator>();

		if (input == null)
			input = GetComponent<GatherInput>();

		// Inicializar estados
		idleState = new IdleState(this);
		runState = new RunState(this);
		jumpState = new JumpState(this);
		fallState = new FallState(this);
		attackState = new AttackState(this);
		castState = new CastState(this);
	}

	public void Knockback()
	{
		if (rb == null) return;

		int direction = transform.localScale.x > 0 ? 1 : -1;
		rb.linearVelocity = new Vector2(knockbackPower.x * -direction, knockbackPower.y);

		if (animator != null)
		{
			animator.SetTrigger("Knockback");
		}
	}

	public bool IsGrounded()
	{
		if (groundCheck == null) return false;

		return Physics2D.OverlapCircle(
			groundCheck.position,
			groundCheckRadius,
			groundLayer
		);
	}

	void Start()
	{
		// Validar que los componentes estén asignados
		if (rb == null)
		{
			Debug.LogError("Player: Rigidbody2D no asignado!", this);
			return;
		}

		if (input == null)
		{
			Debug.LogError("Player: GatherInput no asignado!", this);
			return;
		}

		ChangeState(idleState);
	}

	void Update()
	{
		if (currentState == null) return;
		currentState.Update();
	}

	void FixedUpdate()
	{
		if (currentState == null) return;
		currentState.FixedUpdate();
	}

	public void ChangeState(PlayerState newState)
	{
		if (newState == null)
		{
			Debug.LogError("Player: Intentando cambiar a un estado null!", this);
			return;
		}

		if (currentState == newState) return;

		currentState?.Exit();
		currentState = newState;
		currentState.Enter();
	}
}