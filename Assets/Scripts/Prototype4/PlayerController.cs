using Menu;
using Prototype2;
using System.Collections;
using UnityEngine;

namespace Prototype4
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 10.0f;

		private GameObject _focalPoint;

		private Rigidbody _rigidbody;

		private bool _hasPowerUp = false;
		[SerializeField]
		private float _pushForce = 30.0f;

		[SerializeField]
		private float _powerUpCountdown = 5.0f;

		[SerializeField]
		private GameObject _powerUpIndicator;
		[SerializeField]
		private AudioClip _powerUpSound;

		private AudioSource _audioSource;
		void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_audioSource = GetComponent<AudioSource>();

			_focalPoint = GameObject.Find("Focal Point");
		}

		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			_powerUpIndicator.transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);

			float verticalInput = Input.GetAxis("Vertical");

			_rigidbody.AddForce(_focalPoint.transform.forward * verticalInput * _speed);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("PowerUp"))
			{
				_audioSource.PlayOneShot(_powerUpSound, 1f);
				_hasPowerUp = true;
				_powerUpIndicator.SetActive(true);
				StartCoroutine(PowerUpCountdownRoutine());
				other.GetComponent<PoolableObject>().ReturnCallback();
			}
			else if (other.CompareTag("Dead Zone"))
			{
				Menu.GameManager.Instance.GameOver();
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.CompareTag("Enemy") && _hasPowerUp)
			{
				Vector3 pushDir = (collision.gameObject.transform.position - transform.position).normalized;
				collision.gameObject.GetComponent<Rigidbody>().AddForce(pushDir * _pushForce, ForceMode.Impulse);
			}
		}

		private IEnumerator PowerUpCountdownRoutine()
		{
			yield return new WaitForSeconds(_powerUpCountdown);

			_hasPowerUp = false;
			_powerUpIndicator.SetActive(false);
		}
	}

}