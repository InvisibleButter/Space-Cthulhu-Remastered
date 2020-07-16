using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

	private Vector3 _velocity;
	private Rigidbody _rigidBody;
	private float _xRotation;

	public float MouseSensitivity = 100f;

	void Start () 
	{
		_rigidBody = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void Move(Vector3 velocity) 
	{
		_velocity = velocity;
	}

	public void Rotate() 
	{
		if(!GameController.Instance.IsRunning)
		{
			return;
		}

		float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

		_xRotation -= mouseY;
		_xRotation = Mathf.Clamp(_xRotation, 0f, 0f);
		transform.Rotate(Vector3.up * mouseX);
	}

	public void FixedUpdate()
	{
		if (!GameController.Instance.IsRunning)
		{
			return;
		}

		_rigidBody.MovePosition(_rigidBody.position + _velocity * Time.fixedDeltaTime);
	}
}
