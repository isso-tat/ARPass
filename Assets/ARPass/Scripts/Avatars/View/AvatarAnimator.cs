using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mapbox.Unity.Utilities;
using UnityEngine;

namespace ARPass.Avatars.View
{
	public sealed class AvatarAnimator : MonoBehaviour
	{
		[SerializeField]
		Animation _animation;

		[SerializeField]
		AnimationClip _idle;

		[SerializeField]
		AnimationClip _run;

		Vector2 _currentRotation;

		[SerializeField, Range(0.01f, 0.5f)]
		float _rotationSpeed;

		void Start()
		{
			_animation.AddClip(_idle, AnimationType.Idle.ToString());
			_animation.AddClip(_run, AnimationType.Run.ToString());
			SetAnimation(AnimationType.Idle);
			_animation.Play();
		}
		
		public void Animate(Vector2 direction)
		{
			transform
				.DORotateQuaternion(
					Quaternion.LookRotation(direction.ToVector3xz()),
					_rotationSpeed
				);
			SetAnimation(direction == Vector2.zero ? AnimationType.Idle : AnimationType.Run);
		}

		void SetAnimation(AnimationType animType)
		{
			_animation.CrossFade(animType.ToString());
		}
	}
}