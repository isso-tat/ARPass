using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace ARPass.Start.View {
	public class StartViewController : MonoBehaviour
	{
		[Inject, UsedImplicitly]
		IStartClient _client;

		[SerializeField]
		LoadingView _loading;

		void Start()
		{
			_client
				.CurrentLoaded
				.Subscribe(c => _loading.UpdateStatus((int) c))
				.AddTo(this);
			
			_client.InitialLoad();
		}
	}
}