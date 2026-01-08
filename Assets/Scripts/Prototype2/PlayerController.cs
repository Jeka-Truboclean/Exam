using Menu;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Prototype2
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private PoolableObject _foodPrefab;

		[SerializeField]
		private float _speed = 1.0f;

		[SerializeField]
		private float _bound = 15f;

		private InputAction _moveAction;
		private Vector2 _moveInput;

		private InputAction _fireAction;

		private AudioSource _audioSource;
		[SerializeField]
		private AudioClip _throwSound;
		[SerializeField]
		private AudioClip _stepSound;
		[SerializeField]
		private float _stepRate = 0.5f;

		private float _stepTimer = 0f;

		private ObjectPool<PoolableObject> _pool;

		void Start()
		{
			_pool = new ObjectPool<PoolableObject>(_foodPrefab);
			_audioSource = GetComponent<AudioSource>();

			_moveAction = InputSystem.actions.FindAction("Move");
			_fireAction = InputSystem.actions.FindAction("Fire");

			_moveAction.performed += onMove;
			_moveAction.canceled += onMove;

			_fireAction.performed += onFire;
		}

		private void OnDestroy()
		{
			_moveAction.performed -= onMove;
			_moveAction.canceled -= onMove;

			_fireAction.performed -= onFire;
		}

		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			if (Mathf.Abs(transform.position.x) > _bound)
			{
				transform.position = new Vector3(transform.position.x < 0 ? -_bound : _bound, 0f, 0f);
			}
			if (_moveInput.x != 0)
			{
				_stepTimer -= Time.deltaTime;
				if (_stepTimer <= 0.0f)
				{
					_audioSource.PlayOneShot(_stepSound, 1f);
					_stepTimer = _stepRate;
				}
			}
			else
			{
				_stepTimer = 0.0f;
			}
			transform.Translate(new Vector3(_moveInput.x, 0, 0) * Time.deltaTime * _speed);
		}

		private void onMove(InputAction.CallbackContext context)
		{
			_moveInput = context.ReadValue<Vector2>();
		}

		private void onFire(InputAction.CallbackContext context)
		{
			_audioSource.PlayOneShot(_throwSound, 0.8f);
			var food = _pool.GetObject();

			food.gameObject.transform.position = transform.position;

			food.gameObject.SetActive(true);
		}
	}
}


