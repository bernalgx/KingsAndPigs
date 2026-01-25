using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private Transform m_transform;
	private Rigidbody2D m_rigidbody2D;
	private GatherInput m_gatherInput;
	private Animator m_animator;

	[Header("Gravity Settings")]
	//[SerializeField] private float fallMultiplier = 2.5f;
	//[SerializeField] private float lowJumpMultiplier = 2f;
	[SerializeField] private float fallMultiplier = 2.5f;
	[SerializeField] private float lowJumpMultiplier = 2f;

	private float jumpLockUntil;
	[SerializeField] private float jumpLockDuration = 0.05f;

	private float coyoteTime = 0.1f;
	private float jumpBufferTime = 0.1f;
	private float maxFallSpeed = -50f;

	[Header("Air Momentum")]
	[SerializeField] private float airMomentumDecay = 3f;

	private float airMomentumX;
	private bool hasAirMomentum;

	private float coyoteTimer;
	private float jumpBufferTimer;


	//Animator IDs
	private int idIsGrounded;
	private int idSpeed;
	private int idisWallDetected;
	private int idisKnocked;

	[Header("Move settings")]
	[SerializeField] private float speed;
	private int direction = 1;
	[SerializeField] private bool hasSprintMomentum = false;

	[Header("Jump settings")]
	[SerializeField] private int extraJumps = 1;
	[SerializeField] private float jumpForce;
	[SerializeField] private int counterExtraJumps;

	[Header("Ground settings")]
	[SerializeField] private Transform lFoot;
	[SerializeField] private Transform rFoot;
	RaycastHit2D lFootRay;
	RaycastHit2D rFootRay;
	[SerializeField] private bool isGrounded;
	[SerializeField] private float rayLength;
	[SerializeField] private LayerMask groundLayer;

	//Wall settings
	[SerializeField] private float checkWallDistance;
	[SerializeField] private bool isWallDetected;
	[SerializeField] private bool canWallSlide;
	[SerializeField] private float slideSpeed;
	[SerializeField] Vector2 wallJumpForce;
	[SerializeField] bool isWallJumping;
	[SerializeField] private bool canDoubleJump;
	[SerializeField] private float wallJumpDuration = 0.6f;

	//Knock Settings
	[Header("Knockback Settings")]
	[SerializeField] private bool isKnocked;
	[SerializeField] private bool canBeKnocked;
	[SerializeField] private Vector2 knockbackPower;
	[SerializeField] private float knockedDuration;



	private void Awake()
	{
		m_gatherInput = GetComponent<GatherInput>();
		//m_transform = transform;
		m_rigidbody2D = GetComponent<Rigidbody2D>();
		m_animator = GetComponent<Animator>();
	}


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		idSpeed = Animator.StringToHash("speed");
		idIsGrounded = Animator.StringToHash("isGrounded");
		idisWallDetected = Animator.StringToHash("isWallDetected");
		idisKnocked = Animator.StringToHash("Knockback");
		lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
		rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
		counterExtraJumps = extraJumps;
		GameManager.instance.AddScore();

	}

	void Update()
	{
		SetAnimatorValues();
	}

	private void SetAnimatorValues()
	{
		m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocityX));
		m_animator.SetBool(idIsGrounded, isGrounded);
		m_animator.SetBool(idisWallDetected, isWallDetected);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (isKnocked) return;
		CheckCollision();
		UpdateCoyoteTime();
		UpdateJumpBuffer();
		Move();
		ApplyAirMomentum();
		Jump();
		ApplyBetterJumpGravity();
	}


	private void UpdateCoyoteTime()
	{
		if (isGrounded)
			coyoteTimer = coyoteTime;
		else
			coyoteTimer -= Time.fixedDeltaTime;
	}

	private void UpdateJumpBuffer()
	{
		if (m_gatherInput.IsJumping)
			jumpBufferTimer = jumpBufferTime;
		else
			jumpBufferTimer -= Time.fixedDeltaTime;
	}

	private void CheckCollision()
	{
		HandleGround();
		HandleWall();
		HandleWallSlide();
	}

	private void HandleGround()
	{
		lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLength, groundLayer);
		rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLength, groundLayer);
		if (lFootRay || rFootRay)
		{
			isGrounded = true;
			counterExtraJumps = extraJumps;
			canDoubleJump = false;
		}
		else
		{
			isGrounded = false;
			canDoubleJump = true;
		}
	}

	private void HandleWall()
	{
		isWallDetected = Physics2D.Raycast(m_transform.position, Vector2.right * direction, checkWallDistance, groundLayer);
	}

	private void HandleWallSlide()
	{
		canWallSlide = isWallDetected;
		if (!canWallSlide) return;

		canDoubleJump = false;
		slideSpeed = m_gatherInput.Value.y < 0 ? 1 : 0.5f;
		m_rigidbody2D.linearVelocity =
			new Vector2(m_rigidbody2D.linearVelocityX,
						m_rigidbody2D.linearVelocityY * slideSpeed);
	}

	private void Move()
	{

		if (isWallDetected && !isGrounded) return;
		if (isWallJumping) return;

		Flip();
		m_rigidbody2D.linearVelocity =
			new Vector2(speed * m_gatherInput.Value.x,
						m_rigidbody2D.linearVelocityY);
	}

	private void Flip()
	{
		if (m_gatherInput.Value.x * direction < 0)
		{
			HandleDirection();
		}
	}

	private void HandleDirection()
	{
		m_transform.localScale = new Vector3(-m_transform.localScale.x, 1, 1);
		direction *= -1;
	}



	/// ORO PURO  SILKSONG JUMP////


	/*private bool TryDoJump()
	{
		if (this.hc.cState.dashing || this.hc.cState.airDashing)
		{
			return false;
		}
		if (!this.canJump)
		{
			if (!this.isEnterTumbling)
			{
				this.isJumpQueued = true;
			}
			return false;
		}
		this.entryDelayTime = Time.timeAsDouble + 0.05000000447034836;
		this.isJumpQueued = false;
		this.waterEnterJumpQueueTimeLeft = 0f;
		Vector2 linearVelocity = this.body.linearVelocity;
		bool flag = this.isSprinting;
		this.animator.Play("Airborne");
		this.body.linearVelocity = new Vector2(0f, 10f);
		this.TranslateIfNecessary();
		this.hc.ResetInputQueues();
		if (this.isSprinting)
		{
			this.hc.SetStartWithFlipJump();
		}
		else
		{
			this.hc.SetStartWithJump();
		}
		this.ExitedWater(true);
		if (flag && Math.Abs(linearVelocity.x) > 0.01f)
		{
			this.hc.AddExtraAirMoveVelocity(new HeroController.DecayingVelocity
			{
				Velocity = new Vector2(linearVelocity.x, 0f),
				Decay = 3f,
				CancelOnTurn = true,
				SkipBehaviour = HeroController.DecayingVelocity.SkipBehaviours.WhileMoving
			});
		}
		return true;
	}*/





	//JUMP del curso
	//private void Jump()
	//{
	//	if (m_gatherInput.IsJumping)
	//	{
	//		if (isGrounded)
	//		{
	//			m_rigidbody2D.linearVelocity =
	//				new Vector2(speed * m_gatherInput.Value.x, jumpForce);

	//			canDoubleJump = true;
	//		}
	//		else if (isWallDetected) WallJump();
	//		else if (counterExtraJumps > 0 && canDoubleJump) DoubleJump();
	//	}

	//	m_gatherInput.IsJumping = false;
	//}


	// JumP GPT
	private void Jump()


	{
		// 1️⃣ Si no hay intención almacenada, no hacemos nada
		if (jumpBufferTimer <= 0) return;

		if (Time.time < jumpLockUntil) return;

		// 2️⃣ Ejecutamos el salto cuando sea válido
		if (coyoteTimer > 0)
		{
			m_rigidbody2D.linearVelocity =
				new Vector2(m_rigidbody2D.linearVelocityX, jumpForce);

			canDoubleJump = true;

			ConsumeJump();
		}
		else if (isWallDetected)
		{
			WallJump();
			ConsumeJump();
		}
		else if (counterExtraJumps > 0 && canDoubleJump)
		{
			DoubleJump();
			ConsumeJump();
		}
	}

	private void ApplyAirMomentum()
	{
		if (!hasSprintMomentum) return;
		if (!hasAirMomentum) return;
		if (isGrounded) return;

		airMomentumX = Mathf.Lerp(
			airMomentumX,
			0f,
			Time.fixedDeltaTime * airMomentumDecay
		);

		m_rigidbody2D.linearVelocity =
			new Vector2(
				airMomentumX + speed * m_gatherInput.Value.x,
				m_rigidbody2D.linearVelocityY
			);

		if (Mathf.Abs(airMomentumX) < 0.05f)
		{
			airMomentumX = 0f;
			hasAirMomentum = false;
		}
	}


	private void ConsumeJump()
	{

		// Capturamos el impulso horizontal ACTUAL
		airMomentumX = m_rigidbody2D.linearVelocityX;
		hasAirMomentum = Mathf.Abs(airMomentumX) > 0.01f;

		jumpBufferTimer = 0;
		coyoteTimer = 0;
		m_gatherInput.IsJumping = false;

		jumpLockUntil = Time.time + jumpLockDuration;
	}




	private void ApplyBetterJumpGravity()
	{
		if (m_rigidbody2D.linearVelocityY < 0)
		{
			// Caída más rápida
			m_rigidbody2D.linearVelocity +=
				Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
		}
		else if (!isGrounded &&
				 m_rigidbody2D.linearVelocityY > 0 &&
				 !m_gatherInput.IsJumpHeld)
		{
			Debug.Log("CORTANDO SALTO");
			m_rigidbody2D.linearVelocity +=
				Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
		}
		if (m_rigidbody2D.linearVelocityY < maxFallSpeed)
		{
			m_rigidbody2D.linearVelocity =
				new Vector2(m_rigidbody2D.linearVelocityX, maxFallSpeed);
		}
	}




	private void WallJump()
	{
		m_rigidbody2D.linearVelocity =
			new Vector2(wallJumpForce.x * -direction, wallJumpForce.y);
		HandleDirection();
		StartCoroutine(WallJumpRoutine());
	}

	IEnumerator WallJumpRoutine()
	{
		isWallJumping = true;
		yield return new WaitForSeconds(wallJumpDuration);
		isWallJumping = false;
	}


	private void DoubleJump()
	{
		m_rigidbody2D.linearVelocity =
			new Vector2(speed * m_gatherInput.Value.x, jumpForce);

		counterExtraJumps -= 1;
	}

	public void Knockback()
	{
		StartCoroutine(KnockbackRoutine());
		m_rigidbody2D.linearVelocity =
			new Vector2(knockbackPower.x * -direction, knockbackPower.y);
		m_animator.SetTrigger(idisKnocked);
	}
	private IEnumerator KnockbackRoutine()
	{
		isKnocked = true;
		canBeKnocked = false;
		yield return new WaitForSeconds(knockedDuration);
		isKnocked = false;
		canBeKnocked = true;
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying) return;

		Vector3 pos = transform.position;

		// 🐺 COYOTE TIME (amarillo)
		if (coyoteTimer > 0 && !isGrounded)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(pos + Vector3.up * 0.5f, 0.3f);
		}

		// 🧠 JUMP BUFFER (azul)
		if (jumpBufferTimer > 0)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(pos + Vector3.up * 0.5f, 0.5f);
		}

		// ⬇️ FALL SPEED CLAMP (rojo)
		if (m_rigidbody2D.linearVelocityY <= maxFallSpeed + 0.1f)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pos, pos + Vector3.down * 1.5f);
		}

		// 🧱 WALL CHECK
		Gizmos.color = isWallDetected ? Color.magenta : Color.gray;
		Gizmos.DrawLine(
			pos,
			pos + Vector3.right * direction * checkWallDistance
		);
	}

}
