using Photon.Pun;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public class PhotonConnectionService : MonoBehaviourPunCallbacks, IConnection
	{
		private void Awake() => 
			DontDestroyOnLoad(this);

		public void Connect()
		{
			PhotonNetwork.ConnectUsingSettings();
		}

		public override void OnConnectedToMaster()
		{
			PhotonNetwork.JoinLobby();
			PhotonNetwork.AutomaticallySyncScene = true;
		}
	}
}