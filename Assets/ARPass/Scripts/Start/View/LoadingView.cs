using TMPro;
using UnityEngine;

namespace ARPass.Start.View
{
	public class LoadingView : MonoBehaviour
	{
		[SerializeField]
		TMP_Text _currentLoaded;

		public void UpdateStatus(int currentLoaded)
		{
			_currentLoaded.text = currentLoaded.ToString();
		}
	}
}