using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.Network;
using MGJ.Runtime.UI.Lobby;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure.ApplicationPoints
{
	public class LobbyPoint : MonoBehaviour
	{
		[SerializeField] private Mediator _mediator;
		
		private IConnection _connectionService;

		private void Awake()
		{
			Construct();
		}

		private void Start()
		{
			_mediator.CloseAllMenus();
			_mediator.ShowLoadingScreen();
			_mediator.DisplayLoadingText("Connecting To Network...");
			
			_connectionService.Connect();
		}

		private void Construct() => 
			_connectionService = Container.Services.Resolve<IConnection>();
	}
}