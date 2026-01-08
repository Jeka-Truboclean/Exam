using Menu;
using Prototype2;
using UnityEngine;

namespace Prototype4
{
	public class SpawnManager : MonoBehaviour
	{
		[SerializeField]
		private PoolableObject _prefab;

		[SerializeField]
		private PoolableObject _powerUpPrefab;

		[SerializeField]
		private float _spawnRadius = 10.0f;

		private int _enemyCount;
		private int _waveNumber = 1;

		private ObjectPool<PoolableObject> _enemyPool;
		private ObjectPool<PoolableObject> _powerUpPool;

		void Start()
		{
			_enemyPool = new ObjectPool<PoolableObject>(_prefab);
			_powerUpPool = new ObjectPool<PoolableObject>(_powerUpPrefab);

			SpawnEnemyWave(_waveNumber);
		}

		private void SpawnEnemy()
		{
			Vector3 spawnPos = GenerateRandomPoint();

			var enemy = _enemyPool.GetObject().GetComponent<Enemy>();
			enemy.transform.position = spawnPos;
			enemy.gameObject.SetActive(true);

			_enemyCount++;
			enemy.DeathEvent += () => _enemyCount--;
		}

		private Vector3 GenerateRandomPoint()
		{
			float spawnPosX = Random.Range(-_spawnRadius, _spawnRadius);
			float spawnPosZ = Random.Range(-_spawnRadius, _spawnRadius);
			Vector3 spawnPos = new Vector3(spawnPosX, 0f, spawnPosZ);
			return spawnPos;
		}

		private void SpawnEnemyWave(int spawnCount)
		{
			for (int i = 0; i < spawnCount; i++)
			{
				SpawnEnemy();
			}
			var powerUp = _powerUpPool.GetObject();
			powerUp.transform.position = GenerateRandomPoint();
			powerUp.gameObject.SetActive(true);
		}

		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			if (_enemyCount <= 0)
			{
				SpawnEnemyWave(++_waveNumber);
			}
		}
	}

}