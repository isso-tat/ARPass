using System;
using ARPass.Utils;
using JetBrains.Annotations;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace ARPass.Scenes.Start.View {
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
				.Subscribe(_ => StartARDemo())
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

		async void StartARDemo()
		{
			if (Application.platform == RuntimePlatform.Android) AndroidUtils.ShowToast("Start!");

			await UniTask.Delay(TimeSpan.FromSeconds(3));
			await ARPassSceneManager.Instance.LoadSceneAsync(SceneName.Map);
			_client.SceneLoadFinished();
		}
	}
}