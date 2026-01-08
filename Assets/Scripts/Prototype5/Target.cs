using Prototype2;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Prototype5
{
	public class Target : MonoBehaviour
	{
		private Rigidbody _rb;
		private PoolableObject _poolableObject;

		private float xRange = 4.3f;

		[SerializeField]
		private float _minForce = 10.0f;
		[SerializeField]
		private float _maxForce = 20.0f;

		[SerializeField]
		private float _rangeTorgue = 2.0f;

		[SerializeField]
		private int _score;

		[SerializeField]
		private ParticleSystem _particleSystem;
		[SerializeField]
		private AudioClip _popSound;

		public event Action<int> OnClicked;
		public event Action GameOverEvent;

		public void Init()
		{
			_rb = GetComponent<Rigidbody>();
			_poolableObject = GetComponent<PoolableObject>();

			transform.position = GetRandomPos();
			_rb.linearVelocity = Vector3.zero;
			_rb.angularVelocity = Vector3.zero;

			_rb.AddForce(Vector3.up * GetRandomForce(), ForceMode.Impulse);
			_rb.AddTorque(
				new Vector3(GetRandomTorque(), GetRandomTorque(), GetRandomTorque()),
				ForceMode.Impulse
			);
		}
		private float GetRandomForce()
		{
			return Random.Range(_minForce, _maxForce);
		}

		private Vector3 GetRandomPos()
		{
			return new Vector3(Random.Range(-xRange, xRange), -1.0f);
		}

		private float GetRandomTorque()
		{
			return Random.Range(-_rangeTorgue, _rangeTorgue);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!gameObject.CompareTag("Bad"))
			{
				GameOverEvent?.Invoke();
			}
			_poolableObject.ReturnCallback();
		}

		private void OnMouseDown()
		{
			OnClicked?.Invoke(_score);
			AudioManager.PopSource.PlayOneShot(_popSound, 1.0f);
			Instantiate(_particleSystem, transform.position, Quaternion.identity);
			_poolableObject.ReturnCallback();
		}
	}

}