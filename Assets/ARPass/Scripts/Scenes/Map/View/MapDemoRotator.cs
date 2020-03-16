using Mapbox.Unity.Map.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ARPass.Scenes.Map.View
{
	public sealed class MapDemoRotator : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		[SerializeField]
		Transform _map;

		Vector2 _touchOrigin;

		public void OnPointerDown(PointerEventData eventData)
		{
			_touchOrigin = eventData.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector3 diff = _touchOrigin - eventData.position;
			Vector3 angle = _map.eulerAngles;
			angle.y += diff.y * .3f;
			_map.eulerAngles = angle;
			_touchOrigin = eventData.position;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			throw new System.NotImplementedException();
		}
	}
}