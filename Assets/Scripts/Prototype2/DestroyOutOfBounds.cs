using Menu;
using UnityEngine;

namespace Prototype2
{
	[RequireComponent(typeof(PoolableObject))]
	public class DestroyOutOfBounds : MonoBehaviour
	{
		[SerializeField]
		private float _upperBound = 20.0f;
		[SerializeField]
		private float _lowerBound = -5.0f;

		private PoolableObject _poolable;

		void Start()
		{
			_poolable = GetComponent<PoolableObject>();
		}

		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			if (transform.position.z > _upperBound)
			{
				_poolable.ReturnCallback();
			}
			else if (transform.position.z < _lowerBound)
			{
				_poolable.ReturnCallback();

				GameManager.Instance.GameOver();
			}
		}
	}

}