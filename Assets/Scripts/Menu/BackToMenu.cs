using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
	public class BackToMenu : MonoBehaviour
	{
		private Button _button;
		void Start()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(ReturnToMenu);
		}
		private void ReturnToMenu()
		{
			SceneManager.LoadScene(0);
		}
	}

}