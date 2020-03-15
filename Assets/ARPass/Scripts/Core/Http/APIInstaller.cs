using Zenject;

namespace ARPass.Core.Http
{
	public sealed class APIInstaller : Installer<APIInstaller>
	{
		public override void InstallBindings()
		{
			// TODO: Change the injected host according to environment.

			Container
				.Bind<APIClient>()
				.AsSingle()
				.WithArguments("http://localhost:8001")
				.Lazy();
		}
	}
}