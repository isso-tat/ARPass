using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPass.Start.View
{
	public class LoadingView : MonoBehaviour
	{
		[SerializeField]
		RawImage _image;
		
		[SerializeField]
		TMP_Text _currentLoaded;

		[SerializeField]
		float _hideDuration = .5f;

		public void UpdateStatus(int currentLoaded)
		{
			_currentLoaded.text = currentLoaded.ToString();
		}

		public void Hide()
		{
			DOTween
				.To(
					() => transform.localScale,
					s => transform.localScale = s,
					new Vector3(1.5f, 1.5f),
					_hideDuration);

			DOTween
				.To(
					() => _image.color,
					c => _image.color = c,
					Color.clear,
					_hideDuration);
		}
	}
}