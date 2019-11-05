using UnityEngine;

namespace ARPass.Utils
{
	public static class AndroidUtils
	{
		public static void ShowToast(string toastMessage)
		{
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject unityActivity =
				unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

			if (unityActivity != null)
			{
				AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
				unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
				{
					AndroidJavaObject toastObject =
						toastClass.CallStatic<AndroidJavaObject>(
							"makeText", unityActivity, toastMessage, 0);
					toastObject.Call("show");
				}));
			}
		}
	}
}