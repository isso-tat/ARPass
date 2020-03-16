using System;
using UniRx.Async;

namespace ARPass.Scenes.Authenticate
{
	public sealed class MockAuthenticateClient : IAuthenticateClient
	{
		public async UniTask Login(string username, string password)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(.5f));
		}
	}
}