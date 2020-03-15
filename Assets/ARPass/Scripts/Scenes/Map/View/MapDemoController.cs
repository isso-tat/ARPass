using System;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

namespace ARPass.Scenes.Map.View
{
	public sealed class MapDemoController : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		Vector2d _lonlat;

		[SerializeField, Range(0, 22)]
		int _zoom;

		void Start()
		{
			_map.Initialize(_lonlat, _zoom);
		}
	}
}