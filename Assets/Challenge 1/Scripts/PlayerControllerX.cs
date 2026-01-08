using Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
	[SerializeField]
	private float _speed = 15.0f;
	[SerializeField]
	private float _rotationSpeed = 80.0f;

	private float _verticalInput;

	void Start()
	{

	}

	void FixedUpdate()
	{
		if (!GameManager.Instance._isGameActive)
			return;

		// get the user's vertical input
		_verticalInput = Input.GetAxis("Vertical");

		// move the plane forward at a constant rate
		transform.Translate(Vector3.forward * Time.deltaTime * _speed);

		// tilt the plane up/down based on up/down arrow keys
		transform.Rotate(Vector3.right, _rotationSpeed * Time.deltaTime * _verticalInput);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Obstacle"))
		{
			GameManager.Instance.GameOver();
		}
	}
}
