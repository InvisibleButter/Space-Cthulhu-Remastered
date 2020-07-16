using UnityEngine;

public class Bullet : MonoBehaviour
{
	float speed = 10;
	public LayerMask Mask;
	public int Damage = 1;
	public int LifeTime = 5;

	private float _skinWidth = 0.1f;

	private void Start()
	{
		Destroy(gameObject, LifeTime);

		Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, Mask);
		if (initialCollisions.Length > 0)
		{
			HitObject(initialCollisions[0]);
		}
	}

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	void Update()
	{
		float moveDist = Time.deltaTime * speed;
		//CheckCollision(moveDist);
		transform.Translate(Vector3.forward * moveDist);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			HitObject(other);
		}
	}

	private void CheckCollision(float moveDist)
	{
		Ray r = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(r, out hit, moveDist + _skinWidth, Mask, QueryTriggerInteraction.Collide))
		{
			HitObject(hit.collider);
		}
	}

	private void HitObject(Collider c)
	{
		IDamageable damageableObject = c.GetComponent<IDamageable>();
		if (damageableObject != null)
		{
			damageableObject.Hit(Damage);
		}
		Destroy(gameObject);
	}
}