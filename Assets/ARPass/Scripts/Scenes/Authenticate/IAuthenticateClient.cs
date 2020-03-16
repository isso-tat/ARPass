using UniRx.Async;

namespace ARPass.Scenes.Authenticate
{
	public interface IAuthenticateClient
	{
		UniTask Login(string username, string password);
	}
}