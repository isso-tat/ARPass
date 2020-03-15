using System;
using ARPass.Avatars;
using ARPass.Avatars.View;
using ARPass.Utils;
using JetBrains.Annotations;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UniRx.Async;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ARPass.Scenes.Map.View
{
	public sealed class MapDemoController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		[Inject, UsedImplicitly]
		IMapClient _client;
		
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		Vector2d _lonLatOrigin;

		[SerializeField, Range(1, 22)]
		int _zoom = 18;

		[SerializeField, Range(0, 1)]
		float _speed = 0.2f;

		[SerializeField]
		AvatarAnimator _animator;
		
		Vector2 _touchOrigin;

		Vector2d _current;

		string _state;

		void Start()
		{
			_map.Initialize(_lonLatOrigin, _zoom);
			GUIField.Subscribe(this, 0, _onGui);
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			_touchOrigin = eventData.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			var diff = (eventData.position - _touchOrigin) * _speed;
			_current = _lonLatOrigin - new Vector2d(diff.y, diff.x);
			_animator.Animate(-diff);
			_map.UpdateMap(_current);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			_lonLatOrigin = _current;
			_animator.Animate(Vector2.zero);
		}

		void _onGui()
		{
			if (GUILayout.Button("Save", GUILayout.Height(40)))
			{
				SavePosition().Away();
			}
			GUILayout.Label(_state);
		}

		async UniTask SavePosition()
		{
			_state = "Saving position...";
			await _client
				.SavePosition(new Vector2((float)_current.x, (float)_current.y));
			_state = "Saved!";
		}
	}
}