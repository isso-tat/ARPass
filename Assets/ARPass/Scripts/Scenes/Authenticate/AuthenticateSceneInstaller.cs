using ARPass.Core.Auth;
using ARPass.Core.Http;
using Zenject;

namespace ARPass.Scenes.Authenticate
{
	public sealed class AuthenticateSceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// TODO: Install these classes at the Main scene, in order to use this all over the project.
			AuthInstaller.Install(Container);
			APIInstaller.Install(Container);

			Container
				.Bind<AuthenticateClient>()
				.AsSingle();
		}
	}
}