using ARPass.Http;
using UnityEngine;
using Zenject;

namespace ARPass.Scenes.Start
{
	public class StartInstaller : MonoInstaller
	{
		[SerializeField]
		bool _isMock;

		public override void InstallBindings()
		{
			APIInstaller.Install(Container);

			if (_isMock)
				Container
					.Bind<IStartClient>()
					.To<MockStartClient>()
					.AsSingle();
			else
				Container
					.Bind<IStartClient>()
					.To<StartClient>()
					.AsSingle();
		}
	}
}