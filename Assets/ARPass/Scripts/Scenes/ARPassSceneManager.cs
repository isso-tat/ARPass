using System;
using UniRx.Async;
using UnityEngine.SceneManagement;

namespace ARPass.Scenes
{
	public class ARPassSceneManager
	{
		public static ARPassSceneManager Instance = new ARPassSceneManager();

		public void LoadScene(SceneName scene)
		{
			SceneManager.LoadScene(scene.ToString());
		}

		public async UniTask LoadSceneAsync(SceneName scene)
		{
			await SceneManager.LoadSceneAsync(scene.ToString());
		}
	}
}