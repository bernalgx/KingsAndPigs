using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] private Transform m_transform;
	private Rigidbody2D m_rigidbody2D;
	private GatherInput m_gatherinput;
	private Animator m_animator;

	//Animator IDs
	private int idIsGrounded;
	private int idSpeed;

	[Header("Move settings")]
	[SerializeField] private float speed;
	private int direction = 1;

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


	private void Awake()
	{
		m_gatherinput = GetComponent<GatherInput>();
		//m_transform = GetComponent<Transform>();
		m_rigidbody2D = GetComponent<Rigidbody2D>();
		m_animator = GetComponent<Animator>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		idSpeed = Animator.StringToHash("speed");
		idIsGrounded = Animator.StringToHash("isGrounded");
		lFoot = GameObject.Find("LFoot").GetComponent<Transform>();
		rFoot = GameObject.Find("RFoot").GetComponent<Transform>();
		counterExtraJumps = extraJumps;
	}

	void Update()
	{
		SetAnimatorValues();
	}

	private void SetAnimatorValues()
	{
		m_animator.SetFloat(idSpeed, Mathf.Abs(m_rigidbody2D.linearVelocityX));
		m_animator.SetBool(idIsGrounded, isGrounded);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		CheckCollision();
		Move();
		Jump();
	}
	private void CheckCollision()
	{
		HandleGround();
		HandleWall();
	}

	private void HandleWall()
	{
		isWallDetected = Physics2D.Raycast(m_transform.position, Vector2.right * direction, checkWallDistance, groundLayer);
	}

	private void HandleGround()
	{
		lFootRay = Physics2D.Raycast(lFoot.position, Vector2.down, rayLength, groundLayer);
		rFootRay = Physics2D.Raycast(rFoot.position, Vector2.down, rayLength, groundLayer);
		if (lFootRay || rFootRay)
		{
			isGrounded = true;
			counterExtraJumps = extraJumps;
		}
		else
		{
			isGrounded = false;
		}
	}

	private void Move()
	{
		Flip();
		m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.ValueX, m_rigidbody2D.linearVelocityY);
	}
	private void Flip()
	{
		if (m_gatherinput.ValueX * direction < 0)
		{
			m_transform.localScale = new Vector3(-m_transform.localScale.x, 1, 1);
			direction *= -1;
		}
	}

	private void Jump()
	{
		if (m_gatherinput.IsJumping)
		{
			if (isGrounded)
				m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.ValueX, jumpForce);
			if (counterExtraJumps > 0)
			{

				m_rigidbody2D.linearVelocity = new Vector2(speed * m_gatherinput.ValueX, jumpForce);
				counterExtraJumps--;
			}
		}
		m_gatherinput.IsJumping = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(m_transform.position, new Vector2(m_transform.position.x + checkWallDistance * direction, m_transform.position.y));
	}

}
