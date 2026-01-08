using Prototype2;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype5
{
	public class BallsFactory
	{
		private readonly Dictionary<string, ObjectPool<PoolableObject>> _pools = new();

		public BallsFactory(PoolableObject[] prefabs)
		{
			foreach (var prefab in prefabs)
			{
				_pools[prefab.name] = new ObjectPool<PoolableObject>(prefab);
			}
		}

		public PoolableObject GetBall(string name)
		{
			if (_pools.TryGetValue(name, out var pool))
			{
				return pool.GetObject();
			}
			Debug.LogError($"Ball type '{name}' not found in factory");
			return null;
		}
		public PoolableObject GetRandomBall()
		{
			var keys = new List<string>(_pools.Keys);
			var randomKey = keys[Random.Range(0, _pools.Count)];
			return GetBall(randomKey);
		}
	}
}