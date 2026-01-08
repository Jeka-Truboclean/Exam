using Menu;
using UnityEngine;

namespace Prototype3
{
	public class RepeatBackground : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 10.0f;

		private float _repeatWidth;

		private Vector3 _startPos;

		private BoxCollider _boxCollider;
		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{
			_startPos = transform.position;
			_boxCollider = GetComponent<BoxCollider>();

			_repeatWidth = transform.position.x - _boxCollider.size.x / 2;
		}

		// Update is called once per frame
		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			transform.Translate(Vector3.left * Time.deltaTime * _speed);

			if (transform.position.x < _repeatWidth)
			{
				transform.position = _startPos;
			}
		}
	}

}