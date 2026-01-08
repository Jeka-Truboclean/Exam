using Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge3
{
	public class PlayerControllerX : MonoBehaviour
	{
		public bool gameOver = false;

		[SerializeField]
		private float _upperBound = 20.0f;

		public float bounceForce;
		public float floatForce;
		private float gravityModifier = 1.5f;
		private Rigidbody playerRb;

		public ParticleSystem explosionParticle;
		public ParticleSystem fireworksParticle;

		private AudioSource playerAudio;
		public AudioClip moneySound;
		public AudioClip explodeSound;
		public AudioClip bounceSound;


		// Start is called before the first frame update
		void Start()
		{
			Physics.gravity *= gravityModifier;
			playerAudio = GetComponent<AudioSource>();
			playerRb = GetComponent<Rigidbody>();

			// Apply a small upward force at the start of the game
			playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

		}

		// Update is called once per frame
		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;
			// While space is pressed and player is low enough, float up
			if (Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y < _upperBound)
			{
				
				playerRb.AddForce(Vector3.up * floatForce);
			}
		}

		private void OnCollisionEnter(Collision other)
		{
			// if player collides with bomb, explode and set gameOver to true
			if (other.gameObject.CompareTag("Bomb"))
			{
				explosionParticle.Play();
				playerAudio.PlayOneShot(explodeSound, 1.0f);
				gameOver = true;
				Debug.Log("Game Over!");
				Destroy(other.gameObject);
				GameManager.Instance.GameOver();
			}

			// if player collides with money, fireworks
			else if (other.gameObject.CompareTag("Money"))
			{
				fireworksParticle.Play();
				playerAudio.PlayOneShot(moneySound, 1.0f);
				Destroy(other.gameObject);

			}

			else if (other.gameObject.CompareTag("Ground"))
			{
				playerAudio.PlayOneShot(bounceSound, 1.0f);
				playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
			}
		}

	}

}