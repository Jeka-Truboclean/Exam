using Menu;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Prototype1
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 1.0f;
		[SerializeField]
		private float _turnSpeed = 1.0f;

		private float horizontalInput;
		private float verticalInput;

		private InputAction _moveAction;
		private Vector2 _moveInput;

		void Start()
		{
			_moveAction = InputSystem.actions.FindAction("Move");
			_moveAction.performed += onMove;
			_moveAction.canceled += onMove;
		}

		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			horizontalInput = _moveInput.x;
			verticalInput = _moveInput.y;

			transform.Rotate(Vector3.up, Time.deltaTime * _turnSpeed * horizontalInput);
			transform.Translate(new Vector3(0f, 0f, verticalInput) * Time.deltaTime * _speed);
		}

		private void onMove(InputAction.CallbackContext context)
		{
			_moveInput = context.ReadValue<Vector2>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Dead Zone"))
			{
				GameManager.Instance.GameOver();
			}
		}
	}

}