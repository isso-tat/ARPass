using System;
using ARPass.Http;
using UniRx;
using UniRx.Async;

namespace ARPass.Scenes.Start
{
	public interface IStartClient : IDisposable
	{
		IObservable<float> CurrentLoaded { get; }
		IObservable<APIStatus> OnAuthLoaded { get; }
		UniTask InitialLoad();
		void SceneLoadFinished();
	}
}