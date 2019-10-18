using System;
using System.Collections;
using ARPass.Utils;
using UniRx;
using UnityEngine;

namespace ARPass.Start
{
	public class MockStartClient : IStartClient
	{
		readonly int _loadFrameTime = 200;
		
		readonly Subject<float> _currentLoaded = new Subject<float>();
		public IObservable<float> CurrentLoaded => _currentLoaded;
		
		public void InitialLoad()
		{
			var coroutine = Runner.Instance.StartCoroutine(LoadCoroutine());
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

			yield return null;
		}
	}
}