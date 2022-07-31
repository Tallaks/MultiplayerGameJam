using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
		[FormerlySerializedAs("_roomNameText")] [SerializeField] private TMP_Text _currentRoomNameText;
		[SerializeField] private TMP_Text _playerNameLabel;
		private List<TMP_Text> _allPlayerNames = new();
		
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

		private void Awake() => 
			HideAllUi();

		public void HideAllUi() 
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

		public void ShowMenu() => 
			_menuButtons.SetActive(true);

		public void ShowInputNameScreen()
		{
			HideAllUi();
			_nameInputScreen.SetActive(true);
		}

		public void ShowRoomBrowser()
		{
			HideAllUi();
			_roomBrowserScreen.SetActive(true);
		}

		public void HideRoomBrowser()
		{
			HideAllUi();
			_menuButtons.SetActive(true);
		}

		public void ShowCreateRoomScreen()
		{
			HideAllUi();
			_createRoomScreen.SetActive(true);
		}

		public string GetRoomName() => 
			_roomNameInput.text;
		
		public void CloseErrorScreen() {
			HideAllUi();
			_menuButtons.SetActive(true);
		}

		public void ShowCurrentRoomScreen(string currentRoomName, IEnumerable<string> playerNames,
			bool isMasterClient)
		{
			HideAllUi();
			_roomScreen.SetActive(true);
			_currentRoomNameText.text = currentRoomName;
			
			ListAllPlayers(playerNames);

			_startButton.SetActive(isMasterClient);
		}
		
		private void ListAllPlayers(IEnumerable<string> playerNames) 
		{
			foreach(TMP_Text player in _allPlayerNames) {
				Destroy(player.gameObject);
			}
			_allPlayerNames = new List<TMP_Text>();

			foreach (string playerName in playerNames)
			{
				TMP_Text newPlayerLabel = Instantiate(_playerNameLabel, _playerNameLabel.transform.parent);
				newPlayerLabel.text = playerName;
				newPlayerLabel.gameObject.SetActive(true);
				
				_allPlayerNames.Add(newPlayerLabel);
			}
		}
	}
}