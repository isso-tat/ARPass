using UnityEngine;

namespace ARPass.Utils
{
	public class Runner : MonoBehaviour
	{
		public static readonly Runner Instance = new GameObject("Runner").AddComponent<Runner>();

		void Start()
		{
			DontDestroyOnLoad(Instance);
		}
	}
}