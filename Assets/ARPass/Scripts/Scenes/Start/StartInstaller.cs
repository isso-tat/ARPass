using ARPass.Auth;
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
			// TODO: Install these classes at the Main scene, in order to use this all over the project.
			AuthInstaller.Install(Container);
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