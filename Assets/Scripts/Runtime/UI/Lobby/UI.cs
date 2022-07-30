using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MGJ.Runtime.UI.Lobby
{
	public class UI : MonoBehaviour
	{
		[Header("Menu")] 
		[SerializeField] private GameObject _menuButtons;
		[SerializeField] private GameObject _startButton;
		[SerializeField] private GameObject _roomTestButton;
		
		[Header("Loading Screen")]
		[SerializeField] private GameObject _loadingScreen;
		[SerializeField] private TMP_Text _loadingText;

		[Header("Create Room Screen")] 
		[SerializeField] private GameObject _createRoomScreen;
		[SerializeField] private TMP_InputField _roomNameInput;

		[Header("Room Screen")] 
		[SerializeField] private GameObject _roomScreen;
		[SerializeField] private TMP_Text _roomNameText;
		[SerializeField] private TMP_Text _playerNameLabel;
		private List<TMP_Text> _allPlayerNames;
		
		[Header("Error Screen")]
		[SerializeField] private GameObject _errorScreen;
		[SerializeField] private TMP_Text _errorText;
		
		[Header("Room Browser Screen")]
		[SerializeField] private GameObject _roomBrowserScreen;
		[SerializeField] private RoomButton _roomButton;
		private List<RoomButton> _allRoomButton;

		[Header("Name Input Screen")] 
		[SerializeField] private GameObject _nameInputScreen;
		[SerializeField] private TMP_InputField _nameInput;

		private void Awake()
		{
			CloseMenus();
			
		}

		public void OpenUiScreen(GameObject screen)
		{
			screen.SetActive(true);
		}

		public void CloseUiScreen(GameObject screen)
		{
			screen.SetActive(false);
		}

		public void CloseMenus() 
		{
			_loadingScreen.SetActive(false);
			_menuButtons.SetActive(false);
			_createRoomScreen.SetActive(false);
			_roomScreen.SetActive(false);
			_errorScreen.SetActive(false);
			_roomBrowserScreen.SetActive(false);
			_nameInputScreen.SetActive(false);
		}

		public void ShowLoadingScreen() => 
			_loadingScreen.SetActive(true);
		
		public void DisplayLoadingText(string text) => 
			_loadingText.text = text;
	}
}