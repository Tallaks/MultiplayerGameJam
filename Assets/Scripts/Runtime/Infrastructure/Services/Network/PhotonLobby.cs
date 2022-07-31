using Photon.Pun;
using UnityEngine.Events;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public class PhotonLobby : MonoBehaviourPunCallbacks, ILobby
	{
		public bool NickNameEntered => 
			!string.IsNullOrEmpty(PhotonNetwork.NickName);
		public UnityAction OnJoinedLobbyAction { get; set; }
		
		public void SetNickName(string nickName) => 
			PhotonNetwork.NickName = nickName;

		public override void OnJoinedLobby() => 
			OnJoinedLobbyAction?.Invoke();
	}
}