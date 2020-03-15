using UniRx.Async;
using UnityEngine;

namespace ARPass.Scenes.Map
{
	public interface IMapClient
	{
		UniTask SavePosition(Vector2 position);
	}
}