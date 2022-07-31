using System.Collections.Generic;
using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.Network;
using MGJ.Runtime.Infrastructure.Services.SceneManagement;
using UnityEditor;
using UnityEngine;

namespace MGJ.Runtime.UI.Lobby
{
	public class Mediator : MonoBehaviour
	{
		[SerializeField] private UI _ui;
		
		private ILobby _lobbyService;
		private ISceneLoader _sceneLoader;

		private void Awake()
		{
			_lobbyService = Container.Services.Resolve<ILobby>();
			_sceneLoader = Container.Services.Resolve<ISceneLoader>();
		}

		public void DisplayLoadingText(string text) => 
			_ui.DisplayLoadingText(text);
		
		public void CloseAllMenus() =>
			_ui.HideAllUi();

		public void ShowLoadingScreen() =>
			_ui.ShowLoadingScreen();

		public void ShowMenu() => 
			_ui.ShowMenu();

		public void ShowNameInput() => 
			_ui.ShowInputNameScreen();

		public void ShowRoomBrowser() =>
			_ui.ShowRoomBrowser();

		public void CloseRoomBrowser() =>
			_ui.HideRoomBrowser();

		public void ShowCreateRoomScreen() =>
			_ui.ShowCreateRoomScreen();

		public void CloseErrorScreen() =>
			_ui.CloseErrorScreen();
		
		public void QuitGame()
		{
#if UNITY_EDITOR
			EditorApplication.ExecuteMenuItem("Edit/Play");
#endif
			Application.Quit();
		}
		
		public void CreateRoom() 
		{
			if (!string.IsNullOrEmpty(_ui.GetRoomName())) {
				_lobbyService.CreateRoom(_ui.GetRoomName());

				_ui.HideAllUi();
				DisplayLoadingText("Creating Room...");
				ShowLoadingScreen();
			}
		}

		public void ShowCurrentRoomScreen(string lobbyServiceCurrentRoomName) => 
			_ui.ShowCurrentRoomScreen(lobbyServiceCurrentRoomName, _lobbyService.PlayersNickNames, _lobbyService.IsMasterClient);

		public void UpdatePlayerList() => 
			_ui.ListAllPlayers(_lobbyService.PlayersNickNames);

		public void AddNewPlayerNameToList(string newPlayer) => 
			_ui.AddPlayerToRoomList(newPlayer);

		public void ShowErrorScreen(string message) => 
			_ui.ShowErrorScreen(message);

		public void StartGame() => 
			_sceneLoader.LoadScene("TestScene");

		public void LeaveRoom()
		{
			_lobbyService.LeaveRoom();
			_ui.HideAllUi();
			DisplayLoadingText("Leaving Room");
			ShowLoadingScreen();
		}
	}
}