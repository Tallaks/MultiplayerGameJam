using Photon.Pun;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public class PhotonConnectionService : IConnection
	{
		public void Connect()
		{
			PhotonNetwork.ConnectUsingSettings();
		}
	}
}