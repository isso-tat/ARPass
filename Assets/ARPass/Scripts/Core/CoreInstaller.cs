using ARPass.Core.Auth;
using ARPass.Core.Http;
using ARPass.Core.SceneManagement;
using UnityEngine;
using Zenject;

namespace ARPass.Core
{
	public sealed class CoreInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			SceneManagementInstaller.Install(Container);
			APIInstaller.Install(Container);
			AuthInstaller.Install(Container);
		}
	}
}