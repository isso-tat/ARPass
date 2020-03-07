using System;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;

namespace ARPass.Http
{
	public partial class APIClient
	{
		readonly string _host;
		const int _timeout = 5000;

		public APIClient(string host)
		{
			_host = host;
		}

		string GenerateUri(string path) => $"{_host}/{path}";

		public async UniTask<APIResult> Get(string path, Dictionary<string, string> parameters = null)
		{
			var uri = GenerateUri(path)
				.AddGetParameters(parameters);
			using (var request = UnityWebRequest.Get(uri))
			{
				return await SendRequest(request);
			}
		}

		public async UniTask<APIResult> Post(string path, WWWForm form = null)
		{
			var uri = GenerateUri(path);
			using (var request = UnityWebRequest.Post(uri, form))
			{
				return await SendRequest(request);
			}
		}

		async UniTask<APIResult> SendRequest(UnityWebRequest request)
		{
			try
			{
				await request
				      .SendRequest()
				      .Timeout(TimeSpan.FromMilliseconds(_timeout));
				return request.ToAPIResult();
			}
			catch (TimeoutException)
			{
				// TODO: Impl timeout exception.
				Debug.LogError("API Timeout Exception!!");
				throw;
			}
		}
	}
}