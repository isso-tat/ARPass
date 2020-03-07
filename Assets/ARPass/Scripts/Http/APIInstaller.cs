using Zenject;

namespace ARPass.Http
{
	public sealed class APIInstaller : Installer<APIInstaller>
	{
		public override void InstallBindings()
		{
			var client = new APIClient("http://localhost:8001");

			Container
				.Bind<APIClient>()
				.FromInstance(client)
				.AsSingle()
				.Lazy();
		}
	}
}