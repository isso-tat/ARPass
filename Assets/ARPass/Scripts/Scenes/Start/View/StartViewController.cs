using ARPass.Utils;
using JetBrains.Annotations;
using UniRx;
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

		void Start()
		{
			_client
				.CurrentLoaded
				.Subscribe(c => _loading.UpdateStatus((int) c))
				.AddTo(this);

			_client
				.OnAuthLoaded
				.Subscribe(_ => StartARDemo())
				.AddTo(this);

			_client
				.OnLoadFinished
				.Subscribe(_ => OnLoadFinish())
				.AddTo(this);

			_client.InitialLoad();
		}

		void OnLoadFinish()
		{
			DontDestroyOnLoad(_startCanvas);
			_client.Dispose();
			HideStartView();
		}

		async void HideStartView()
		{
			await _loading.Hide();
			Destroy(_startCanvas);
		}

		async void StartARDemo()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidUtils.ShowToast("Start!");
			}

			await ARPassSceneManager.Instance.LoadSceneAsync(SceneName.Map);
			_client.SceneLoadFinished();
		}
	}
}