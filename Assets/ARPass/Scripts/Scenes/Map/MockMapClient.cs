using System;
using UniRx.Async;
using UnityEngine;

namespace ARPass.Scenes.Map
{
	public sealed class MockMapClient : IMapClient
	{
		public async UniTask SavePosition(Vector2 position)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(.5f));
		}
	}
}