using System;
using UniRx;

namespace ARPass.Scenes.Start
{
	public interface IStartClient : IDisposable
	{
		IObservable<float> CurrentLoaded { get; }
		IObservable<Unit> OnLoadFinished { get; } 
		void InitialLoad();
	}
}