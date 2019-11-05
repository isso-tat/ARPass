using UniRx.Async;
using UnityEngine;

namespace ARPass.Utils
{
	public static class UniRxUtils
	{
		public static void Away(this UniTask self)
		{
			self.Forget(Debug.LogException);
		}

		public static void Away<T>(this UniTask<T> self)
		{
			self.Forget(Debug.LogException);
		}
	}
}