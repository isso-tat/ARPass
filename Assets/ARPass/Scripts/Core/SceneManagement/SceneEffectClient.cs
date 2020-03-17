using System;
using UniRx;
using UniRx.Async;

namespace ARPass.Core.SceneManagement
{
	public sealed class SceneEffectClient : IDisposable
	{
		readonly Subject<bool> _onFade = new Subject<bool>();
		public IObservable<bool> OnFadeChangeAsObservable => _onFade;
		
		public float EffectLength { get; private set; }

		public async UniTask Fade(bool fade, float sec)
		{
			EffectLength = sec;
			_onFade.OnNext(fade);
			await UniTask.Delay(TimeSpan.FromSeconds(sec));
		}

		public void Dispose()
		{
			_onFade?.Dispose();
		}
	}
}