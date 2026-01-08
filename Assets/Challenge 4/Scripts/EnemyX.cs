using Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge4
{
	public class EnemyX : MonoBehaviour
	{
		public float speed = 100.0f;
		private Rigidbody enemyRb;
		private GameObject playerGoal;

		private GameObject _spawnManager;

		// Start is called before the first frame update
		void Start()
		{
			enemyRb = GetComponent<Rigidbody>();
			_spawnManager = GameObject.Find("Spawn Manager");
			speed = _spawnManager.GetComponent<SpawnManagerX>()._enemySpeed;

			playerGoal = GameObject.Find("Player Goal");
		}

		// Update is called once per frame
		void Update()
		{
			if (!GameManager.Instance._isGameActive)
				return;

			// Set enemy direction towards player goal and move there
			Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
			enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

		}

		private void OnCollisionEnter(Collision other)
		{
			// If enemy collides with either goal, destroy it
			if (other.gameObject.name == "Enemy Goal")
			{
				Destroy(gameObject);
			}
			else if (other.gameObject.name == "Player Goal")
			{
				Destroy(gameObject);
				GameManager.Instance.GameOver();
			}

		}

	}
}
