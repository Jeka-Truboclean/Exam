using UnityEngine;

namespace Prototype4
{
	public class RotateCamera : MonoBehaviour
	{
		[SerializeField]
		private float _rotationSpeed = 150.0f;
		
		void Update()
		{
			float horizontalInput = Input.GetAxis("Horizontal");

			transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * _rotationSpeed);
		}
	}

}