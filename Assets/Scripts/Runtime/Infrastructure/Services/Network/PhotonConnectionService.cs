using Photon.Pun;
using UnityEngine.Events;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public class PhotonConnectionService : MonoBehaviourPunCallbacks, IConnection
	{
		private void Awake() => 
			DontDestroyOnLoad(this);

		public UnityAction OnConnectedAction { get; set; }

		public void Connect() => 
			PhotonNetwork.ConnectUsingSettings();

		public override void OnConnectedToMaster()
		{
			PhotonNetwork.JoinLobby();
			PhotonNetwork.AutomaticallySyncScene = true;
			OnConnectedAction?.Invoke();
		}
	}
}