using System;
using UniRx;

namespace ARPass.Start
{
	public class StartClient : IStartClient
	{
		public IObservable<float> CurrentLoaded { get; }
		public IObservable<Unit> OnLoadFinished { get; }

		public void InitialLoad()
		{
			
		}

		public void Dispose()
		{
		}
	}
}