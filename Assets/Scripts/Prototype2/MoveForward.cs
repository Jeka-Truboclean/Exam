using Menu;
using UnityEngine;

namespace Prototype2
{
	public class MoveForward : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 1.0f;
		void Start()
		{

		}

		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			transform.Translate(Vector3.forward * Time.deltaTime * _speed);
		}
	}

}