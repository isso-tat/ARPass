using DG.Tweening;
using UniRx.Async;

namespace ARPass.Utils
{
	public static class DoTweenUtils
	{
		public static IAwaiter<Tween> GetAwaiter(this Tween self)
		{
			var source = new UniTaskCompletionSource<Tween>();

			void OnComplete()
			{
				self.onKill -= OnComplete;
				source.TrySetResult(self);
			}

			self.onKill += OnComplete;

			return source.Task.GetAwaiter();
		}
	}
}