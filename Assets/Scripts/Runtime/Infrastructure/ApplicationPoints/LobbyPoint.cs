using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.Network;
using MGJ.Runtime.UI.Lobby;
using UnityEngine;
using UnityEngine.Serialization;

namespace MGJ.Runtime.Infrastructure.ApplicationPoints
{
	public class LobbyPoint : MonoBehaviour
	{
		[FormerlySerializedAs("_mediator")] [SerializeField] private Mediator _uiMediator;
		
		private IConnection _connectionService;
		private ILobby _lobbyService;

		private void Awake()
		{
			Construct();
		}

		private void Start()
		{
			_connectionService.OnConnectedAction += () => _uiMediator.DisplayLoadingText("Joining Lobby...");
			_lobbyService.OnJoinedLobbyAction += OnJoinedLobby;
			_lobbyService.OnJoinedRoomAction += OnJoinedRoom;
			
			_uiMediator.CloseAllMenus();
			_uiMediator.ShowLoadingScreen();
			_uiMediator.DisplayLoadingText("Connecting To Network...");
			
			_connectionService.Connect();
		}

		private void OnJoinedRoom()
		{
			_uiMediator.ShowCurrentRoomScreen(_lobbyService.CurrentRoomName);
		}

		private void Construct()
		{
			_connectionService = Container.Services.Resolve<IConnection>();
			_lobbyService = Container.Services.Resolve<ILobby>();
		}

		private void OnJoinedLobby() 
		{
			_uiMediator.CloseAllMenus();
			_uiMediator.ShowMenu();

			if (!_lobbyService.NickNameEntered)
			{
				_lobbyService.SetNickName(Random.Range(0, 1000).ToString());
				_uiMediator.ShowNameInput();
			}
		}
	}
}