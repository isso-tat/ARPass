using System;

namespace ARPass.Start
{
	public class StartClient : IStartClient
	{
		public IObservable<float> CurrentLoaded { get; }

		public void InitialLoad()
		{
			
		}
	}
}