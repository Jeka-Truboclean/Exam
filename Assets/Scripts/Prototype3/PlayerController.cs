using Menu;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Prototype3
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private float _jumpForce = 500.0f;
		[SerializeField]
		private float _gravityModifier = 9.81f;

		private Rigidbody _rb;
		private Animator _animator;
		private AudioSource _audioSource;

		[SerializeField]
		private ParticleSystem _explosionEffect;
		[SerializeField]
		private ParticleSystem _dirtEffect;

		[SerializeField]
		private AudioClip _crashSound;
		[SerializeField]
		private AudioClip _jumpSound;

		private InputAction _jumpAction;

		private bool _isGrounded = true;

		private Vector3 _originalGravity;

		void Start()
		{
			_animator = GetComponent<Animator>();
			_audioSource = GetComponent<AudioSource>();

			_rb = GetComponent<Rigidbody>();

			_originalGravity = Physics.gravity;

			Physics.gravity *= _gravityModifier;

			_jumpAction = InputSystem.actions.FindAction("Jump");
			_jumpAction.performed += OnJump;
		}

		private void OnDestroy()
		{
			Physics.gravity = _originalGravity;

			if (_jumpAction != null)
				_jumpAction.performed -= OnJump;
		}

		private void OnJump(InputAction.CallbackContext context)
		{
			if (_isGrounded && GameManager.Instance._isGameActive)
			{
				_audioSource.PlayOneShot(_jumpSound, 1.0f);
				_dirtEffect.Stop();
				_animator.SetTrigger("Jump_trig");
				_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
				_isGrounded = false;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Ground"))
			{
				if (GameManager.Instance._isGameActive)
				{
					_dirtEffect.Play();
				}
				_isGrounded = true;
			}
			else if (collision.gameObject.CompareTag("Obstacle"))
			{
				_audioSource.PlayOneShot(_crashSound, 1.0f);
				_dirtEffect.Stop();
				_explosionEffect.Play();
				_animator.SetBool("Death_b", true);
				_animator.SetInteger("DeathType_int", 1);
				GameManager.Instance.GameOver();
			}
		}
	}

}