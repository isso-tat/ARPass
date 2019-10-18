using System;

namespace ARPass.Start
{
	public interface IStartClient
	{
		IObservable<float> CurrentLoaded { get; }
		void InitialLoad();
	}
}