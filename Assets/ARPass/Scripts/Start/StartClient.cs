using System;
using ARPass.Utils;
using UniRx;
using UniRx.Async;

namespace ARPass.Start
{
	public class StartClient : IStartClient
	{
		readonly Subject<float> _currentLoaded = new Subject<float>();
		readonly Subject<Unit> _loadFinished = new Subject<Unit>();

		public IObservable<float> CurrentLoaded => _currentLoaded;
		public IObservable<Unit> OnLoadFinished => _loadFinished;

		public void InitialLoad()
		{
			_currentLoaded.OnNext(1);
			FirebaseInit().Away();
			_currentLoaded.OnNext(100);
		}

		async UniTask FirebaseInit()
		{
			await Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
			{
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
					UnityEngine.Debug.LogError(System.String.Format(
						"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
					// Firebase Unity SDK is not safe to use here.
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