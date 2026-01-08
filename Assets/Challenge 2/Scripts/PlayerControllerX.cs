using Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge2
{
	public class PlayerControllerX : MonoBehaviour
	{
		public GameObject dogPrefab;

		[SerializeField]
		private float _cooldown = 1.0f;

		private float _nextAvailableTime;

		[SerializeField]
		private AudioClip _sendDogSound;

		private AudioSource _audioSource;
		// Update is called once per frame
		private void Start()
		{
			_audioSource = GetComponent<AudioSource>();
		}
		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			// On spacebar press, send dog
			if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextAvailableTime)
			{
				_audioSource.PlayOneShot(_sendDogSound, 0.8f);
				Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
				_nextAvailableTime = Time.time + _cooldown;
			}
		}
	}
}
