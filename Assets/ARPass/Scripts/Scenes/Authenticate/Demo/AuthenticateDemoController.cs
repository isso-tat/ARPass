using ARPass.Core.Http;
using ARPass.Core.SceneManagement;
using ARPass.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace ARPass.Scenes.Authenticate.Demo
{
	public sealed class AuthenticateDemoController : MonoBehaviour
	{
		[Inject, UsedImplicitly]
		IAuthenticateClient _client;

		[Inject, UsedImplicitly]
		ARPassSceneManager _sceneManager;

		[Inject, UsedImplicitly]
		SceneEffectClient _sceneEffect;

		void Start()
		{
			DebugUtils.Log("start auth.");
			GUIField.Subscribe(this, 0, _onGUI);
		}

		void _onGUI()
		{
			if (GUILayout.Button("Auto log in", GUILayout.Height(40))) Login();
		}
		
		async void Login()
		{
			try
			{
				await _client.Login("Foo@gmail.com", "Bar123");
				await _sceneEffect.Fade(fade: true, .5f);
				await _sceneManager.LoadScene(SceneName.Map);
				_sceneEffect.Fade(fade: false, .75f).Away();
			}
			catch (APIException e)
			{
				if (e.Status == APIStatus.Unauthorized) Debug.LogError("The default email and password is wrong.");
			}
		}
	}
}