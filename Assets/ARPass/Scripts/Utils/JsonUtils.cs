using Newtonsoft.Json;
using UniRx.Async;

namespace ARPass.Utils
{
	public static class JsonUtils
	{
		public static async UniTask<T> DeserializeJson<T>(this string json)
		{
			await UniTask.SwitchToThreadPool();
			var instance = JsonConvert.DeserializeObject<T>(json);
			await UniTask.SwitchToMainThread();
			return instance;
		}
	}
}