#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MGJ.Editor
{
	public class PlayerPrefsCleaner
	{
		[MenuItem("CustomTools/Delete Prefs")]
		public static void CleanPrefs()
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
#endif