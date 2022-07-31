using System.Collections.Generic;
using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.Coroutines;
using MGJ.Runtime.Infrastructure.Services.Network;
using MGJ.Runtime.Infrastructure.Services.SceneManagement;
using MGJ.Runtime.UI.Lobby;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MGJ.Runtime.Infrastructure.ApplicationPoints
{
	public class LobbyPoint : MonoBehaviour
	{
		[FormerlySerializedAs("_mediator")] [SerializeField] private Mediator _uiMediator;
		
		private IConnection _connectionService;
		private ILobby _lobbyService;
		private ISceneLoader _sceneLoader;

		private void Construct()
		{
			Container.Services.
				Bind<ISceneLoader>().
				To<PhotonSceneLoader>().
				FromMethod(() => new PhotonSceneLoader(Container.Services.Resolve<ICoroutineRunner>()));

			_sceneLoader = Container.Services.Resolve<ISceneLoader>();
			_connectionService = Container.Services.Resolve<IConnection>();
			_lobbyService = Container.Services.Resolve<ILobby>();
		}

		private void Awake() => 
			Construct();

		private void Start()
		{
			_connectionService.OnConnectedAction += () => _uiMediator.DisplayLoadingText("Joining Lobby...");
			_lobbyService.OnJoinedLobbyAction += OnJoinedLobby;
			_lobbyService.OnJoinedRoomAction += OnJoinedRoom;
			_lobbyService.OnPlayerEnteredRoomAction += OnPlayerEnteredRoom;
			_lobbyService.OnPlayerLeftRoomAction += OnPlayerLeftRoom;
			_lobbyService.OnCreateRoomFailedAction += OnCreateRoomFailed;
			_lobbyService.OnLeftRoomAction += OnLeftRoom;
			_lobbyService.OnRoomListUpdateAction += OnUpdateList;
			
			_uiMediator.ShowLoadingScreen();
			
			_connectionService.Connect();
		}

		private void OnDestroy()
		{
			_connectionService.OnConnectedAction -= () => _uiMediator.DisplayLoadingText("Joining Lobby...");
			_lobbyService.OnJoinedLobbyAction -= OnJoinedLobby;
			_lobbyService.OnJoinedRoomAction -= OnJoinedRoom;
			_lobbyService.OnPlayerEnteredRoomAction -= OnPlayerEnteredRoom;
			_lobbyService.OnPlayerLeftRoomAction -= OnPlayerLeftRoom;
			_lobbyService.OnCreateRoomFailedAction -= OnCreateRoomFailed;
			_lobbyService.OnLeftRoomAction -= OnLeftRoom;
			_lobbyService.OnRoomListUpdateAction -= OnUpdateList;
		}

		private void OnUpdateList(IEnumerable<RoomDecorator> roomList)
		{
			_uiMediator.UpdateRoomList(roomList);
			


			// Create room button if room is not full and the room exists

		}

		private void OnLeftRoom() => 
			_uiMediator.ShowMenu();

		private void OnCreateRoomFailed(short retCode, string message) => 
			_uiMediator.ShowErrorScreen(message);

		private void OnPlayerLeftRoom() => 
			_uiMediator.UpdatePlayerList();

		private void OnPlayerEnteredRoom(string newPlayer) => 
			_uiMediator.AddNewPlayerNameToList(newPlayer);

		private void OnJoinedRoom() => 
			_uiMediator.ShowCurrentRoomScreen(_lobbyService.CurrentRoomName);

		private void OnJoinedLobby() 
		{
			_uiMediator.ShowMenu();

			if (!_lobbyService.NickNameEntered)
			{
				_lobbyService.SetNickName(Random.Range(0, 1000).ToString());
				_uiMediator.ShowNameInput();
			}
		}
	}
}