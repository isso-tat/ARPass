using Zenject;

namespace ARPass.Core.Auth
{
	public sealed class AuthInstaller : Installer<AuthInstaller>
	{
		public override void InstallBindings()
		{
			var repository = new AuthRepository();

			Container
				.Bind<AuthRepository>()
				.FromInstance(repository)
				.AsSingle();
		}
	}
}