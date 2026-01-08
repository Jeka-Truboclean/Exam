using Prototype2;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
	public class AnimalFactory
	{
		private readonly Dictionary<string, ObjectPool<PoolableObject>> _pools = new();

		public AnimalFactory(PoolableObject[] prefabs)
		{
			foreach (var prefab in prefabs)
			{
				_pools[prefab.name] = new ObjectPool<PoolableObject>(prefab);
			}
		}

		public PoolableObject GetAnimal(string name)
		{
			if (_pools.TryGetValue(name, out var pool))
			{
				return pool.GetObject();
			}
			Debug.LogError($"Animal type '{name}' not found in factory");
			return null;
		}
		public PoolableObject GetRandomAnimal()
		{
			var keys = new List<string>(_pools.Keys);
			var randomKey = keys[Random.Range(0, _pools.Count)];
			return GetAnimal(randomKey);
		}
	}

}