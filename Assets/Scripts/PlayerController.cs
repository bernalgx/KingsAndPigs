using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : MonoBehaviour
{

	// 1. CONFIG / SETTINGS (Serialized Fields)
	// 2. STATE (runtime variables)
	// 3. UNITY LIFECYCLE (Awake / Start / Update / FixedUpdate)
	// 4. INPUT & TIMERS
	// 5. COLLISION CHECKS
	// 6. MOVEMENT (ground / air)
	// 7. JUMP SYSTEM
	// 8. WALL SYSTEM
	// 9. PHYSICS MODIFIERS (gravity, clamp)
	// 10. COMBAT / KNOCKBACK
	// 11. DEBUG


	[SerializeField] private GameObject lapaMissilePrefab;
	[SerializeField] private Transform spellSpawnPoint;
	[Header("Spell - Lapa")]
	[SerializeField] private float spellCooldown = 0.6f;
	private float spellCooldownTimer;


	[Header("Components")]
	[SerializeField] private Transform m_transform;
	private Rigidbody2D m_rigidbody2D;
	private GatherInput m_gatherInput;
	private Animator m_animator;

	[Header("Attack Settings")]
	[SerializeField] private float attackDuration = 0.15f;
	[SerializeField] private Vector2 attackBoxSize = new Vector2(1.2f, 0.6f);
	[SerializeField] private Vector2 attackBoxOffset = new Vector2(0.8f, 0f);
	[SerializeField] private LayerMask enemyLayer;


	private bool isAttacking;

	[Header("Dash Settings")]
	[SerializeField] private float dashSpeed = 20f;
	[SerializeField] private float dashDuration = 0.15f;
	[SerializeField] private float dashCooldown = 0.3f;

	private bool isDashing;
	private float dashCooldownTimer;

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


	private bool ignoreGravityOneFrame;
	[SerializeField] private int maxAirDashes = 1;
	private int airDashCount;




	private void Awake()
	{

		if (spellSpawnPoint == null)
			Debug.LogError("SpellSpawnPoint no asignado en PlayerController");

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

	//void Update()
	//{

	//	if (spellCooldownTimer > 0)
	//		spellCooldownTimer -= Time.deltaTime;
	//	if (m_gatherInput.IsSpelling && spellCooldownTimer <= 0)
	//	{
	//		CastLapaSpell();
	//	}


	//	SetAnimatorValues();


	//	if (m_gatherInput.IsAttacking && !isAttacking && !isDashing)
	//	{
	//		StartCoroutine(AttackRoutine());
	//	}

	//	if (dashCooldownTimer > 0)
	//		dashCooldownTimer -= Time.deltaTime;

	//	if (m_gatherInput.IsDashing
	//&& !isDashing
	//&& dashCooldownTimer <= 0
	//&& (isGrounded || airDashCount < maxAirDashes))
	//	{
	//		// si estoy atacando, lo cancelo
	//		if (isAttacking)
	//		{
	//			StopAllCoroutines();
	//			isAttacking = false;
	//			m_animator.SetBool("isAttacking", false);
	//			m_gatherInput.IsAttacking = false;
	//		}

	//		StartCoroutine(DashRoutine());
	//	}





	//}

	void Update()
	{
		// 1️⃣ Timers
		UpdateTimers();

		// 2️⃣ Animator (no decide nada)
		SetAnimatorValues();

		// 3️⃣ Acciones fuertes
		HandleDash();

		// 4️⃣ Acciones medias
		HandleAttack();

		// 5️⃣ Acciones ligeras
		HandleSpell();
	}




	private void UpdateTimers()
	{
		if (spellCooldownTimer > 0)
			spellCooldownTimer -= Time.deltaTime;

		if (dashCooldownTimer > 0)
			dashCooldownTimer -= Time.deltaTime;
	}

	private void HandleDash()
	{
		if (!m_gatherInput.IsDashing) return;
		if (isDashing) return;
		if (dashCooldownTimer > 0) return;
		if (!isGrounded && airDashCount >= maxAirDashes) return;

		// cancelar ataque si existe
		if (isAttacking)
		{
			StopAllCoroutines();
			isAttacking = false;
			m_animator.SetBool("isAttacking", false);
			m_gatherInput.IsAttacking = false;
		}

		StartCoroutine(DashRoutine());
	}

	private void HandleAttack()
	{
		if (!m_gatherInput.IsAttacking) return;
		if (isAttacking) return;
		if (isDashing) return;

		StartCoroutine(AttackRoutine());
	}

	private void HandleSpell()
	{
		if (!m_gatherInput.IsSpelling) return;
		if (spellCooldownTimer > 0) return;

		CastLapaSpell();
	}













	private void CastLapaSpell()
	{

		if (lapaMissilePrefab == null)
		{
			Debug.LogError("LapaMissilePrefab NO asignado");
			return;
		}



		spellCooldownTimer = spellCooldown;

		Instantiate(
			lapaMissilePrefab,
			spellSpawnPoint.position,
			Quaternion.Euler(0, 0, direction == 1 ? 0 : 180)
		);

		// consumimos input
		m_gatherInput.IsSpelling = false;
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

		if (ignoreGravityOneFrame)
		{
			ignoreGravityOneFrame = false;
			return; // 🔥 NO gravedad, NO física este frame
		}

		if (isDashing) return;
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

			airDashCount = 0; // 🔥 reset dash aéreo
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
		// Solo wall slide si:
		// - hay pared
		// - no estás en el suelo
		// - estás cayendo (o en el tope del salto)
		if (!isWallDetected) return;
		if (isGrounded) return;
		if (m_rigidbody2D.linearVelocityY > 0) return;

		canWallSlide = true;

		slideSpeed = m_gatherInput.Value.y < 0 ? 1f : 0.5f;
		m_rigidbody2D.linearVelocity =
			new Vector2(
				m_rigidbody2D.linearVelocityX,
				m_rigidbody2D.linearVelocityY * slideSpeed
			);
	}


	private void Move()
	{

		if (isWallDetected && m_gatherInput.Value.x == direction)
			return;

		//if (isWallDetected && !isGrounded) return;
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

	private IEnumerator DashRoutine()
	{
		// 🔹 1. SNAP: ignorar física 1 frame
		ignoreGravityOneFrame = true;

		// 🔹 2. Resolver pared ANTES
		if (isWallDetected)
		{
			direction *= -1;
			m_transform.localScale = new Vector3(-m_transform.localScale.x, 1, 1);
			canWallSlide = false;
			isWallDetected = false;
		}

		// 🔹 3. Entrar en dash
		isDashing = true;
		dashCooldownTimer = dashCooldown;

		m_animator.SetBool("isDashing", true);

		m_rigidbody2D.linearVelocity = Vector2.zero;

		float timer = 0f;

		while (timer < dashDuration)
		{
			m_rigidbody2D.linearVelocity =
				new Vector2(direction * dashSpeed, 0f);

			timer += Time.deltaTime;
			yield return null;
		}

		// 🔹 4. Salida limpia
		isDashing = false;
		m_animator.SetBool("isDashing", false);
		m_gatherInput.IsDashing = false;
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

	private IEnumerator AttackRoutine()
	{

		isAttacking = true;

		m_rigidbody2D.linearVelocity =
			new Vector2(0f, m_rigidbody2D.linearVelocityY);

		Vector2 attackPos =
			(Vector2)transform.position +
			new Vector2(attackBoxOffset.x * direction, attackBoxOffset.y);

		Collider2D[] hits = Physics2D.OverlapBoxAll(
			attackPos,
			attackBoxSize,
			0f,
			enemyLayer
		);
		m_animator.SetBool("isAttacking", true);


		foreach (Collider2D hit in hits)
		{
			if (hit.TryGetComponent(out Enemy enemy))
			{
				enemy.TakeDamage(1);
			}
		}

		yield return new WaitForSeconds(attackDuration);

		isAttacking = false;
		m_gatherInput.IsAttacking = false; // 👈 CLAVE
		m_animator.SetBool("isAttacking", false);
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
