using System;
using System.Collections;
using ARPass.Utils;
using UniRx;

namespace ARPass.Scenes.Start
{
	public class MockStartClient : IStartClient
	{
		readonly int _loadFrameTime = 200;
		
		readonly Subject<float> _currentLoaded = new Subject<float>();
		readonly Subject<Unit> _loadFinished = new Subject<Unit>();
		
		public IObservable<float> CurrentLoaded => _currentLoaded;
		public IObservable<Unit> OnLoadFinished => _loadFinished;
		
		public void InitialLoad()
		{
			Runner.Instance.StartCoroutine(LoadCoroutine());
		}

		IEnumerator LoadCoroutine()
		{
			var i = 0;
			
			while (i < _loadFrameTime)
			{
				i++;
				// ReSharper disable once PossibleLossOfFraction
				_currentLoaded.OnNext(i * 100 / _loadFrameTime);
				yield return i / _loadFrameTime;
			}

			_loadFinished.OnNext(Unit.Default);
			yield return null;
		}

		public void Dispose()
		{
			_currentLoaded?.Dispose();
			_loadFinished?.Dispose();
		}
	}
}