#if UNITY_EDITOR
using MGJ.Runtime.UI.MainMenu;
using UnityEditor;
using UnityEngine;

namespace MGJ.Editor
{
	[CustomEditor(typeof(Mediator))]
	public class MainMenuMediatorEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			var mediatorScript = (Mediator)target;
			
			if (GUILayout.Button("Enter Lobby"))
			{
				if(EditorApplication.isPlaying)
					mediatorScript.EnterLobby();
			}
			
			if (GUILayout.Button("Quit Game")) 
				if(EditorApplication.isPlaying)
					mediatorScript.QuitGame();
		}
	}
}
#endif