using ARPass.Core.SceneManagement;
using ARPass.Utils;
using DG.Tweening;
using JetBrains.Annotations;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ARPass.Scenes.Core
{
	public sealed class CoreSceneController : MonoBehaviour
	{
		[Inject, UsedImplicitly]
		ARPassSceneManager _manager;
		
		[Inject, UsedImplicitly]
		SceneEffectClient _sceneEffectClient;

		[SerializeField]
		RawImage _fade;

		bool _faded;

		void Start()
		{
			_sceneEffectClient
				.OnFadeChangeAsObservable
				.Where(fade => fade != _faded)
				.Subscribe(fade => SetFade(fade, _sceneEffectClient.EffectLength).Away())
				.AddTo(this);
		}

		async UniTask SetFade(bool fade, float sec)
		{
			_faded = fade;
			await DOTween
				.ToAlpha(
					() => _fade.color,
					c => _fade.color = c,
					fade ? 1 : 0,
					sec
				)
				.SetEase(Ease.Flash);
		}
	}
}