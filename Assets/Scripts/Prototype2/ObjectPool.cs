using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
	public class ObjectPool<T> where T : PoolableObject
	{
		private T _prefab;

		private Queue<T> _queue;

		public ObjectPool(T prefab)
		{
			_prefab = prefab;
			_queue = new Queue<T>();
		}

		public T GetObject()
		{
			T obj = _queue.Count > 0 ? _queue.Dequeue() : Object.Instantiate(_prefab);
			obj.ReturnCallback = () => { ReturnObject(obj); };
			return obj;
		}

		public void ReturnObject(T obj)
		{
			obj.gameObject.SetActive(false);
			_queue.Enqueue(obj);
		}
	}

}