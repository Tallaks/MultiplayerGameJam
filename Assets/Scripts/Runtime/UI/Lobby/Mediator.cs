using UnityEngine;

namespace MGJ.Runtime.UI.Lobby
{
	public class Mediator : MonoBehaviour
	{
		[SerializeField] private UI _ui;

		public void DisplayLoadingText(string text) => 
			_ui.DisplayLoadingText(text);
		
		public void CloseAllMenus() =>
			_ui.CloseMenus();

		public void ShowLoadingScreen() =>
			_ui.ShowLoadingScreen();
	}
}