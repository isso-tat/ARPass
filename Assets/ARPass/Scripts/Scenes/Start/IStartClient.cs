using System;
using UniRx;
using UniRx.Async;

namespace ARPass.Scenes.Start
{
	public interface IStartClient : IDisposable
	{
		IObservable<float> CurrentLoaded { get; }
		IObservable<Unit> OnAuthLoaded { get; }
		UniTask InitialLoad();
		void SceneLoadFinished();
	}
}