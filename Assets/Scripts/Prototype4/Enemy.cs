using Menu;
using Prototype2;
using System;
using UnityEngine;

namespace Prototype4
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 7.0f;

		public event Action DeathEvent;

		private GameObject _player;

		private Rigidbody _rigidBody;

		private PoolableObject _poolableObject;
		void Start()
		{

			_rigidBody = GetComponent<Rigidbody>();
			_poolableObject = GetComponent<PoolableObject>();

			_player = GameObject.Find("Player");
		}

		private void OnEnable()
		{
			DeathEvent = null;
			if (_rigidBody)
			{
				_rigidBody.linearVelocity = Vector3.zero;
				_rigidBody.angularVelocity = Vector3.zero;
			}
		}

		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			Vector3 lookDir = (_player.transform.position - transform.position).normalized;
			_rigidBody.AddForce(lookDir * _speed);

			if(transform.position.y < -15f)
			{
				DeathEvent?.Invoke();
				_poolableObject.ReturnCallback();
			}
		}
	}

}