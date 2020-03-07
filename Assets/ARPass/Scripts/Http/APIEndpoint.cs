using UniRx.Async;
using UnityEngine;

namespace ARPass.Http
{
	public partial class APIClient
	{
		public async UniTask<string> Test()
		{
			var response = await Get("test/a");
			return response.Data;
		}

		public async UniTask<APIResult> FetchMe()
		{
			var response = await Get("me");
			return response;
		}
	}
}