using ARPass.Core.Auth;
using ARPass.Core.Http;
using Zenject;

namespace ARPass.Scenes.Authenticate
{
	public sealed class AuthenticateSceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<AuthenticateClient>()
				.AsSingle();
		}
	}
}