using ARPass.Core.Auth;
using ARPass.Utils;
using UniRx.Async;
using UnityEngine;

namespace ARPass.Core.Http
{
	public partial class APIClient
	{
		public async UniTask<AuthEntity> FetchMe()
		{
			var response = await Get("me");
			return await response.Data.DeserializeJson<AuthEntity>();
		}

		public async UniTask<LoginResult> Login(string username, string password)
		{
			var form = new WWWForm();
			form.AddField("username", username);
			form.AddField("password", password);
			var response = await Post("login", form);
			return await response.Data.DeserializeJson<LoginResult>();
		}
	}
}