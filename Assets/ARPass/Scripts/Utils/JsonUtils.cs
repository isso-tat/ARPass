using Newtonsoft.Json;
using UniRx.Async;

namespace ARPass.Utils
{
	public static class JsonUtils
	{
		public static T DeserializeJson<T>(this string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}