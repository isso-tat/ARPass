using ARPass.Core.Auth;
using ARPass.Core.Http;
using UnityEngine;
using Zenject;

namespace ARPass.Scenes.Authenticate
{
	public sealed class AuthenticateSceneInstaller : MonoInstaller
	{
		[SerializeField]
		bool _isMock;

		public override void InstallBindings()
		{
			if (_isMock)
				Container
					.Bind<IAuthenticateClient>()
					.To<MockAuthenticateClient>()
					.AsSingle();
			else
				Container
					.Bind<IAuthenticateClient>()
					.To<AuthenticateClient>()
					.AsSingle();
		}
	}
}