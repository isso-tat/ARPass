using System;
using ARPass.Auth;
using ARPass.Http;
using ARPass.Utils;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace ARPass.Scenes.Start
{
	public class StartClient : IStartClient
	{
		readonly Subject<float> _currentLoaded = new Subject<float>();
		readonly Subject<APIStatus> _authLoadedFinished = new Subject<APIStatus>();
		readonly Subject<Unit> _loadFinished = new Subject<Unit>();

		public IObservable<float> CurrentLoaded => _currentLoaded;
		public IObservable<APIStatus> OnAuthLoaded => _authLoadedFinished;
		public IObservable<Unit> OnLoadFinished => _loadFinished;

		readonly APIClient _apiClient;
		readonly AuthRepository _authRepository;

		bool _sceneLoaded;

		public StartClient(APIClient apiClient, AuthRepository authRepository)
		{
			_apiClient = apiClient;
			_authRepository = authRepository;
		}

		public async UniTask InitialLoad()
		{
			_currentLoaded.OnNext(1);
			await FirebaseInit();
			_currentLoaded.OnNext(50);
			await SaveAuth();
			_currentLoaded.OnNext(90);
			await UniTask.WaitUntil(() => _sceneLoaded);
			_currentLoaded.OnNext(100);
		}

		public void SceneLoadFinished()
		{
			_sceneLoaded = true;
		}

		async UniTask FirebaseInit()
		{
			await Firebase
			      .FirebaseApp
			      .CheckAndFixDependenciesAsync()
			      .ContinueWith(task => {
				      var dependencyStatus = task.Result;
				      if (dependencyStatus == Firebase.DependencyStatus.Available)
				      {
					      // Create and hold a reference to your FirebaseApp,
					      // where app is a Firebase.FirebaseApp property of your application class.
					      //   app = Firebase.FirebaseApp.DefaultInstance;

					      // Set a flag here to indicate whether Firebase is ready to use by your app.
				      }
				      else
				      {
					      // Firebase Unity SDK is not safe to use here.
					      Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
				      }
			      });
		}

		public void Dispose()
		{
			_currentLoaded?.Dispose();
			_loadFinished?.Dispose();
		}

		async UniTask SaveAuth()
		{
			try
			{
				var result = await _apiClient.FetchMe();
				_authRepository.SaveMe(result);
				_authLoadedFinished.OnNext(APIStatus.OK);
			}
			catch (APIException e)
			{
				_authLoadedFinished.OnNext(e.Status);
			}
			
		}
	}
}