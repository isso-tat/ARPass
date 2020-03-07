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
		AuthRepository _authRepository;

		const int _authLoadFrameTime = 120;
		const int _sceneLoadFrameTime = 80;
		const int _authLoadPercent = 60;
		const int _sceneLoadPercent = 39;
		
		readonly Subject<float> _currentLoaded = new Subject<float>();
		readonly Subject<Unit> _authLoaded = new Subject<Unit>();
		
		public IObservable<float> CurrentLoaded => _currentLoaded;
		public IObservable<Unit> OnAuthLoaded => _authLoaded;

		bool _sceneLoadFinished;

		public MockStartClient(AuthRepository authRepository)
		{
			_authRepository = authRepository;
		}

		public async UniTask InitialLoad()
		{
			var authEntity = Resources.Load<TextAsset>("mockauth").ToString().DeserializeJson<AuthEntity>();
			await Runner.Instance.StartCoroutine(LoadCoroutine(0, _authLoadFrameTime, _authLoadPercent));
			_authRepository.SaveMe(authEntity);
			_authLoaded.OnNext(Unit.Default);
			await Runner.Instance.StartCoroutine(LoadCoroutine(_authLoadPercent, _sceneLoadFrameTime, _sceneLoadPercent));
			await UniTask.WaitUntil(() => _sceneLoadFinished);
			_currentLoaded.OnNext(100);
		}

		public void SceneLoadFinished()
		{
			_sceneLoadFinished = true;
		}

		IEnumerator LoadCoroutine(float initialRate, int maxFrameTime, int maxLoadPercent)
		{
			var i = 0;

			while (i < maxFrameTime)
			{
				i++;
				
				var percent = (int) initialRate + i * maxLoadPercent / maxFrameTime;
				_currentLoaded.OnNext(percent);
				
				yield return i / _authLoadFrameTime;
			}
		}

		public void Dispose()
		{
			_currentLoaded?.Dispose();
			_authLoaded?.Dispose();
		}
	}
}