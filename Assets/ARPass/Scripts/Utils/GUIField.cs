using System;
using System.Collections;
using System.Collections.Specialized;
using UniRx;
using UnityEngine;

namespace ARPass.Utils
{
	public sealed class GUIField : MonoBehaviour
	{
		static readonly GUIField instance = new GameObject().AddComponent<GUIField>();

		[SerializeField]
		bool _enableGui = true;

		bool _foldAll;

		readonly OrderedDictionary[] _entries =
		{
			new OrderedDictionary(),
			new OrderedDictionary(),
			new OrderedDictionary(),
		};

		void OnGUI()
		{
#if !FONDI_BUILD_RELEASE
			if (!_enableGui) return;

			// Space for safe area.
			GUILayout.Space(30);

			GUI.skin.label.fontSize = 9;
			GUI.skin.toggle.fontSize = 9;

			GUILayout.BeginHorizontal();

			var rowIndex = 0;
			foreach (var entries in _entries) // raw
			{
				GUILayout.BeginVertical();

				if (rowIndex++ == 1) // middle row
				{
					_foldAll = GUILayout.Toggle(_foldAll, "Fold All");
				}

				foreach (DictionaryEntry kv in entries)
				{
					var v = (Tuple) kv.Value;

					GUILayout.BeginVertical("box");
					v.Enabled = GUILayout.Toggle(v.Enabled, v.Name);

					if (v.Enabled && !_foldAll)
					{
						try
						{
							v.OnGui();
						}
						catch (Exception e)
						{
							Debug.LogException(e);
						}
					}

					GUILayout.EndVertical();
				}

				GUILayout.EndVertical();
			}

			GUILayout.EndHorizontal();
#endif
		}

		public static void Subscribe(Component owner, int group, Action onGui) => instance.DoRegister(owner, group, onGui);

		void DoRegister(Component owner, int group, Action onGui)
		{
			var entries = _entries[group];
			entries[owner] = new Tuple(owner.GetType().Name, () => onGui(), true);
			Disposable.Create(() => entries.Remove(owner)).AddTo(owner);
		}

		class Tuple
		{
			public readonly Action OnGui;
			public readonly string Name;
			public bool Enabled;

			public Tuple(string name, Action onGui, bool enabled)
			{
				Name = name;
				OnGui = onGui;
				Enabled = enabled;
			}
		}
	}
}