using System;
using ARPass.Http;
using ARPass.Utils;
using JetBrains.Annotations;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace ARPass.Scenes.Start.View
{
	public class StartViewController : MonoBehaviour
	{
		[Inject, UsedImplicitly]
		IStartClient _client;

		[SerializeField]
		GameObject _startCanvas;

		[SerializeField]
		LoadingView _loading;

		async void Start()
		{
			DontDestroyOnLoad(_startCanvas);

			_client
				.CurrentLoaded
				.Subscribe(c => _loading.UpdateStatus((int) c))
				.AddTo(this);

			_client
				.OnAuthLoaded
				.Subscribe(NextScene)
				.AddTo(this);

			await _client.InitialLoad();

			OnLoadFinish();
		}

		async void OnLoadFinish()
		{
			_client.Dispose();
			await _loading.Hide();
			Destroy(_startCanvas);
		}

		async void NextScene(APIStatus status)
		{
			if (Application.platform == RuntimePlatform.Android) AndroidUtils.ShowToast("Start!");
			switch (status)
			{
				case APIStatus.OK:
					await ARPassSceneManager.Instance.LoadSceneAsync(SceneName.Map);
					break;
				case APIStatus.Unauthorized:
					await ARPassSceneManager.Instance.LoadSceneAsync(SceneName.Authenticate);
					break;
				default:
					ShowError(status);
					return;
			}

			// Wait for 1 frame to make sure the next scene is loaded.
			// Otherwise hide animation doesn't work seemlessly.
			await UniTask.DelayFrame(1);

			_client.SceneLoadFinished();
		}

		void ShowError(APIStatus status)
		{
			// TODO: Impl error handling.
			Debug.LogError($"Something wrong with loading auth info! status: {status}");
		}
	}
}