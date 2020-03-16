using UnityEngine;

namespace ARPass.Utils
{
	public static class DebugUtils
	{
		public static void Log(string log)
		{
#if PRODUCTION
			return;
#else
			Debug.Log(log);
#endif
		}
	}
}