using Menu;
using Prototype2;
using UnityEngine;

namespace Prototype3
{
	public class SpawnManager : MonoBehaviour
	{
		[SerializeField]
		private PoolableObject _prefab;

		[SerializeField]
		private Vector3 _spawnPos = new(23f, 0f, 0f);

		[SerializeField]
		private float _startDelay = 2.0f;
		[SerializeField]
		private float _spawnInterval = 1.5f;

		private ObjectPool<PoolableObject> _pool;
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_pool = new ObjectPool<PoolableObject>(_prefab);

			InvokeRepeating(nameof(SpawnObstacle), _startDelay, _spawnInterval);
		}

		// Update is called once per frame
		void Update()
		{

		}

		private void SpawnObstacle()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			var obstacle = _pool.GetObject();
			obstacle.transform.position = _spawnPos;
			obstacle.gameObject.SetActive(true);
		}
	}

}