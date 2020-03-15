using System;
using System.Collections.Generic;
using ARPass.Core.Auth;
using ARPass.Utils;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;

namespace ARPass.Core.Http
{
	public partial class APIClient
	{
		readonly AuthRepository _auth;

		readonly string _host;
		const int _timeout = 5000;

		public APIClient(string host, AuthRepository authRepository)
		{
			_host = host;
			_auth = authRepository;
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
			DebugUtils.Log($"API {request.method}: {request.uri}");
			SetHeaders(ref request);
			try
			{
				await request
				      .SendRequest()
				      .Timeout(TimeSpan.FromMilliseconds(_timeout));
				return request.ToAPIResult();
			}
			catch (TimeoutException)
			{
				throw new APIException(APIStatus.Timeout, "API Timeout Error.");
			}
		}

		void SetHeaders(ref UnityWebRequest request)
		{
			// If token is "", the response code is 400, which is not suitable to detect unauthorized error.
			var token = string.IsNullOrEmpty(_auth.AccessToken) ? "missing" : _auth.AccessToken;
			request.SetRequestHeader("Authorization", $"Bearer {token}");
			request.SetRequestHeader("Accept", "application/json");
			request.SetRequestHeader("Cache-Control", "no-cache");
		}
	}
}