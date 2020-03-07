using System;
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
		readonly Subject<Unit> _authLoadFinished = new Subject<Unit>();
		readonly Subject<Unit> _loadFinished = new Subject<Unit>();

		public IObservable<float> CurrentLoaded => _currentLoaded;
		public IObservable<Unit> OnAuthLoaded => _authLoadFinished;
		public IObservable<Unit> OnLoadFinished => _loadFinished;

		readonly APIClient _apiClient;

		public StartClient(APIClient apiClient)
		{
			_apiClient = apiClient;
		}

		public async UniTask InitialLoad()
		{
			_currentLoaded.OnNext(1);
			await FirebaseInit();
			_currentLoaded.OnNext(50);
			Debug.Log(await _apiClient.Test());
			_currentLoaded.OnNext(100);
		}

		public void SceneLoadFinished()
		{
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
	}
}