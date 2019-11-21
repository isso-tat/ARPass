using System;
using System.Collections;
using ARPass.Auth;
using ARPass.Utils;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace ARPass.Scenes.Start
{
	public class MockStartClient : IStartClient
	{
		readonly int _loadFrameTime = 200;
		
		readonly Subject<float> _currentLoaded = new Subject<float>();
		readonly Subject<Unit> _authLoaded = new Subject<Unit>();
		readonly Subject<Unit> _loadFinished = new Subject<Unit>();
		
		public IObservable<float> CurrentLoaded => _currentLoaded;
		public IObservable<Unit> OnAuthLoaded => _authLoaded;
		public IObservable<Unit> OnLoadFinished => _loadFinished;

		bool _authLoadFinished;
		bool _sceneLoadFinished;

		public async void InitialLoad()
		{
			await Runner.Instance.StartCoroutine(LoadCoroutine());
			var authEntity = Resources.Load<TextAsset>("mockauth").ToString().DeserializeJson<AuthEntity>();
			AuthRepository.Instance.SaveAuth(authEntity);
			_authLoadFinished = true;
			_authLoaded.OnNext(Unit.Default);
		}

		public void SceneLoadFinished()
		{
			_sceneLoadFinished = true;
		}

		IEnumerator LoadCoroutine()
		{
			var i = 0;

			while (i < _loadFrameTime)
			{
				i++;
				
				var percent = i * 100 / _loadFrameTime;
				_currentLoaded.OnNext(percent);

				if (percent == 60)
				{
					UniTask.WaitUntil(() => _authLoadFinished);
				}

				if (percent == 80)
				{
					UniTask.WaitUntil(() => _sceneLoadFinished);
				}
				
				yield return i / _loadFrameTime;
			}

			_loadFinished.OnNext(Unit.Default);
		}

		public void Dispose()
		{
			_currentLoaded?.Dispose();
			_loadFinished?.Dispose();
		}
	}
}