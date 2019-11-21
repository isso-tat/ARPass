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
			SceneManager.LoadScene(GetSceneName(scene));
		}

		public async UniTask LoadSceneAsync(SceneName scene)
		{
			await SceneManager.LoadSceneAsync(GetSceneName(scene));
		}

		string GetSceneName(SceneName scene)
		{
			switch (scene)
			{
				case SceneName.Start:
					return "Start";
				case SceneName.Map:
					return "Map";
				default:
					throw new Exception($"Scene name not defined: { scene }");
			}
		}
	}
}