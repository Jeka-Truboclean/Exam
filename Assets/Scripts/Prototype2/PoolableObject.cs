using System;
using UnityEngine;

namespace Prototype2
{
	public class PoolableObject : MonoBehaviour
	{
		public Action ReturnCallback { get; set; }
	}

}