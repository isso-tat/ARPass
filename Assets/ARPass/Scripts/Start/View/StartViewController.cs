using ARPass.Utils;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ARPass.Start.View {
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
				.OnLoadFinished
				.Subscribe(_ => OnLoadFinish())
				.AddTo(this);

			_client.InitialLoad();
		}

		async void OnLoadFinish()
		{
			DontDestroyOnLoad(_startCanvas);
			_client.Dispose();
			HideStartView();
			StartARDemo();
		}

		async void HideStartView()
		{
			await _loading.Hide();
			Destroy(_startCanvas);
		}

		void StartARDemo()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidUtils.ShowToast("Start AR Demo!");
			}
			
			SceneManager.LoadSceneAsync("ARCoreDemo");
		}
	}
}