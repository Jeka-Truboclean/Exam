using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
	public class UIManager : MonoBehaviour
	{
		public void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public void ToMenu()
		{
			SceneManager.LoadScene(0);
		}
	}
}
