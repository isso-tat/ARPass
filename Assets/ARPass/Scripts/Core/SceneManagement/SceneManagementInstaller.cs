using Zenject;

namespace ARPass.Core.SceneManagement
{
	public sealed class SceneManagementInstaller : Installer<SceneManagementInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<ARPassSceneManager>()
				.AsSingle();
		}
	}
}