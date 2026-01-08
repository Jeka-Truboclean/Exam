using Prototype2;
using Prototype5;
using TMPro;
using UnityEngine;

namespace Menu
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		[SerializeField]
		private GameObject _gameOverPanel;
		public bool _isGameActive = true;

		private void Awake()
		{
			if (Instance == null)
			{
				StartGame();
				Instance = this;
			}
			else
				Destroy(gameObject);
		}

		public void StartGame()
		{
			_isGameActive = true;
			_gameOverPanel.SetActive(false);
		}

		public void GameOver()
		{
			_isGameActive = false;
			_gameOverPanel.SetActive(true);
		}
	}
}
