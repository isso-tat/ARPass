using System;
using UniRx.Async;
using UnityEngine.SceneManagement;
using Zenject;

namespace ARPass.Core.SceneManagement
{
	public class ARPassSceneManager : IInitializable
	{
		public SceneName CurrentScene { get; private set; }

		public void Initialize()
		{
			Enum.TryParse<SceneName>(SceneManager.GetActiveScene().name, out var currentScene);
			CurrentScene = currentScene;
			SceneManager
				.LoadSceneAsync("Core", LoadSceneMode.Additive);
		}

		public async UniTask LoadScene(SceneName scene)
		{
			await SceneManager.UnloadSceneAsync(CurrentScene.ToString());
			await SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
		}

		public async UniTask LoadSceneAsync(SceneName scene)
		{
			UniTask unload = SceneManager
				.UnloadSceneAsync(CurrentScene.ToString())
				.ToUniTask();
			UniTask load = SceneManager
				.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive)
				.ToUniTask();
			CurrentScene = scene;
			await UniTask.WhenAll(unload, load);
		}

		
	}
}