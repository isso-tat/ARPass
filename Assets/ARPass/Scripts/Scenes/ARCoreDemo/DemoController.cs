using System;
using GoogleARCore;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ARPass.Scenes.ARCoreDemo
{
	public class DemoController : MonoBehaviour
	{
		[SerializeField]
		Camera _firstPersonCamera;

		[SerializeField]
		GameObject _spotPrefab;

		const float prefabRotation = 180.0f;

		void Awake()
		{
			Application.targetFrameRate = 60;
		}

		void Update()
		{
			Touch touch;
			if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
			{
				return;
			}
			
			if (EventSystem.current?.IsPointerOverGameObject(touch.fingerId) ?? false)
			{
				return;
			}
			
			Debug.Log("Unity: touch starting....");

			TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
			                                  TrackableHitFlags.FeaturePointWithSurfaceNormal;

			// ReSharper disable once Unity.PerformanceCriticalCodeInvocation
			if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out var hit))
			{
				Debug.Log("Unity: extracted hit....");
				// Use hit pose and camera pose to check if hittest is from the
				// back of the plane, if it is, no need to create the anchor.
				if ((hit.Trackable is DetectedPlane) &&
				    Vector3.Dot(_firstPersonCamera.transform.position - hit.Pose.position,
					    hit.Pose.rotation * Vector3.up) < 0)
				{
					Debug.Log("Hit at back of the current DetectedPlane");
				}
				else
				{
//                    // Choose the prefab based on the Trackable that got hit.
//                    GameObject prefab;
//                    if (hit.Trackable is FeaturePoint)
//                    {
//                        prefab = GameObjectPointPrefab;
//                    }
//                    else if (hit.Trackable is DetectedPlane)
//                    {
//                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
//                        if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
//                        {
//                            prefab = GameObjectVerticalPlanePrefab;
//                        }
//                        else
//                        {
//                            prefab = GameObjectHorizontalPlanePrefab;
//                        }
//                    }
//                    else
//                    {
//                        prefab = GameObjectHorizontalPlanePrefab;
//                    }

					var spot = Instantiate(_spotPrefab, hit.Pose.position, hit.Pose.rotation);

					spot.transform.Rotate(0, prefabRotation, 0, Space.Self);

					// ReSharper disable once Unity.PerformanceCriticalCodeInvocation
					var anchor = hit.Trackable.CreateAnchor(hit.Pose);

					spot.transform.parent = anchor.transform;
				}
			}
		}
	}
}