using Menu;
using Prototype2;
using UnityEngine;

namespace Prototype3
{
	public class MoveLeft : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 5.0f;

		[SerializeField]
		private float _leftBound = -10.0f;

		private PoolableObject _poolable;
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_poolable = GetComponent<PoolableObject>();
		}

		// Update is called once per frame
		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			transform.Translate(Vector3.left * Time.deltaTime * _speed);

			if (transform.position.x < _leftBound)
			{
				_poolable.ReturnCallback();
			}
		}
	}
}
