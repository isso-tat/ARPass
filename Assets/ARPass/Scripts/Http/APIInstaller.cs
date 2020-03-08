using Zenject;

namespace ARPass.Http
{
	public sealed class APIInstaller : Installer<APIInstaller>
	{
		public override void InstallBindings()
		{
			// TODO: Change the injected host according to environment.

			Container
				.Bind<string>()
				.FromInstance("http://localhost:8001")
				.WhenInjectedInto<APIClient>();

			Container
				.Bind<APIClient>()
				.AsSingle()
				.Lazy();
		}
	}
}