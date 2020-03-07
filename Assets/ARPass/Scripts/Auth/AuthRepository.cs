using UnityEngine;

namespace ARPass.Auth
{
	public class AuthRepository
	{
		const string _accessTokenPrefKey = "accessToken";

		AuthEntity _entity;
		public AuthEntity Entity => _entity;

		string _accessToken;
		public string AccessToken => _accessToken;

		public AuthRepository()
		{
			_accessToken = PlayerPrefs.GetString(_accessTokenPrefKey);
		}

		public void SaveMe(AuthEntity entity)
		{
			_entity = entity;
		}

		public void SaveAccessToken(string token)
		{
			_accessToken = token;
			PlayerPrefs.SetString(_accessTokenPrefKey, token);
		}
	}
}