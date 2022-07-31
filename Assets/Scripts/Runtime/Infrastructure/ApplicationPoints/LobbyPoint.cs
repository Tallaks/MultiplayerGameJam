using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.Network;
using MGJ.Runtime.UI.Lobby;
using Photon.Pun;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure.ApplicationPoints
{
	public class LobbyPoint : MonoBehaviour
	{
		[SerializeField] private Mediator _mediator;
		
		private IConnection _connectionService;
		private ILobby _lobbyService;

		private void Awake()
		{
			Construct();
		}

		private void Start()
		{
			_connectionService.OnConnectedAction += () => _mediator.DisplayLoadingText("Joining Lobby...");
			_lobbyService.OnJoinedLobbyAction += OnJoinedLobby;
			
			_mediator.CloseAllMenus();
			_mediator.ShowLoadingScreen();
			_mediator.DisplayLoadingText("Connecting To Network...");
			
			_connectionService.Connect();
		}

		private void Construct()
		{
			_connectionService = Container.Services.Resolve<IConnection>();
			_lobbyService = Container.Services.Resolve<ILobby>();
		}

		private void OnJoinedLobby() 
		{
			_mediator.CloseAllMenus();
			_mediator.ShowMenu();

			Debug.Log(PhotonNetwork.NickName);
			
			if (!_lobbyService.NickNameEntered)
			{
				_lobbyService.SetNickName(Random.Range(0, 1000).ToString());
				_mediator.CloseAllMenus();
				_mediator.ShowNameInput();
			}
		}
	}
}