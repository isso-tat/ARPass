using ARPass.Auth;
using ARPass.Http;
using UniRx.Async;
using UnityEngine;

namespace ARPass.Scenes.Authenticate
{
	public sealed class AuthenticateClient
	{
		readonly APIClient _apiClient;
		readonly AuthRepository _authRepository;

		public AuthenticateClient(APIClient apiClient, AuthRepository authRepository)
		{
			_apiClient = apiClient;
			_authRepository = authRepository;
		}

		public async UniTask Login(string username, string password)
		{
			var result = await _apiClient.Login(username, password);
			_authRepository.SaveAccessToken(result.Token);
			var me = await _apiClient.FetchMe();
			_authRepository.SaveMe(me);
		}
	}
}