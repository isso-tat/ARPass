using ARPass.Core.Auth;
using ARPass.Core.Http;
using UnityEngine;
using Zenject;

namespace ARPass.Scenes.Start
{
	public class StartSceneInstaller : MonoInstaller
	{
		[SerializeField]
		bool _isMock;

		public override void InstallBindings()
		{
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