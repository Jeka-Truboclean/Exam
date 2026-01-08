using UnityEngine;

namespace Prototype5
{
	public class AudioManager : MonoBehaviour
	{
		public static AudioSource PopSource;

		[SerializeField]
		private AudioSource _popSource;

		private void Awake()
		{
			PopSource = _popSource;
		}
	}

}