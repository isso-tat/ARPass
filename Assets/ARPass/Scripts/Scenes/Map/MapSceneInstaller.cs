using Zenject;

namespace ARPass.Scenes.Map
{
	public sealed class MapSceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IMapClient>()
				.To<MockMapClient>()
				.AsSingle();
		}
	}
}