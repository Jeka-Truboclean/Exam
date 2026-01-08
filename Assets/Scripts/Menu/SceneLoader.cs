using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
	public class SceneLoader : MonoBehaviour
	{
		public void LoadScene(int index)
		{
			SceneManager.LoadScene(index);
		}
	}

}