using NUnit.Framework;
using Prototype2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype5
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _scoreTMP;

		private int _score = 0;

		[SerializeField]
		private GameObject _gameOverPanel;
		private bool _isGameActive = false;

		[SerializeField]
		private PoolableObject[] _prefabs;

		[SerializeField]
		private float _spawnRate = 1.0f;

		private BallsFactory _ballsFactory;

		public void StartGame(int difficulty)
		{
			_ballsFactory = new BallsFactory(_prefabs);

			_spawnRate /= difficulty;

			_isGameActive = true;

			_scoreTMP.gameObject.SetActive(true);
			_scoreTMP.text = $"Score: {_score}";

			StartCoroutine(SpawnTargetCoroutine());
		}

		private void UpdateScore(int scoreToAdd)
		{
			_score += scoreToAdd;
			_scoreTMP.text = $"Score: {_score}";
		}

		// Update is called once per frame
		private IEnumerator SpawnTargetCoroutine()
		{
			while (_isGameActive)
			{
				yield return new WaitForSeconds(_spawnRate);

				var target = _ballsFactory.GetRandomBall().GetComponent<Target>();
				target.gameObject.SetActive(true);
				target.Init();

				target.OnClicked -= UpdateScore;

				target.OnClicked += UpdateScore;
				target.GameOverEvent += () =>
				{
					_gameOverPanel.SetActive(true);
					_isGameActive = false;
				};
			}
		}
	}

}