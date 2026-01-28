using UnityEngine;

public class LapaMissile : MonoBehaviour
{
	[SerializeField] private float speed = 8f;
	[SerializeField] private float rotateSpeed = 200f;
	[SerializeField] private float lifeTime = 4f;
	[SerializeField] private LayerMask enemyLayer;
	[SerializeField] private float detectRadius = 6f;
	[SerializeField] private int damage = 1;


	[SerializeField] private float retargetCheckInterval = 0.2f;
	private float retargetTimer;

	[SerializeField] private int maxChains = 3;
	private int currentChains;


	private Transform target;




	void Start()
	{
		FindTarget();
		Destroy(gameObject, lifeTime);
	}

	void FixedUpdate()
	{
		retargetTimer -= Time.fixedDeltaTime;

		if (target == null || retargetTimer <= 0)
		{
			FindTarget();
			retargetTimer = retargetCheckInterval;
		}

		if (target == null)
		{
			// vuelo recto si no hay target
			transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
			return;
		}


		Vector2 dir = (Vector2)target.position - (Vector2)transform.position;
		dir.Normalize();

		float rotateAmount = Vector3.Cross(dir, transform.right).z;
		transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.fixedDeltaTime);

		transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
	}

	void FindTarget()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(
			transform.position,
			detectRadius,
			enemyLayer
		);

		float closestDist = Mathf.Infinity;

		foreach (Collider2D hit in hits)
		{
			float dist = Vector2.Distance(transform.position, hit.transform.position);
			if (dist < closestDist)
			{
				closestDist = dist;
				target = hit.transform;
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Enemy"))
			return;

		if (other.TryGetComponent(out Enemy enemy))
		{
			bool enemyDied = enemy.TakeDamage(damage);

			if (enemyDied)
			{
				currentChains++;

				if (currentChains >= maxChains)
				{
					Destroy(gameObject);
					return;
				}

				target = null;
				FindTarget();

				if (target == null)
				{
					Destroy(gameObject);
				}

				return;
			}

		}

		// si NO murió → el misil se destruye
		Destroy(gameObject);
	}


	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, detectRadius);
	}


}
