using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public class PhotonLobby : MonoBehaviourPunCallbacks, ILobby
	{
		public bool NickNameEntered => 
			!string.IsNullOrEmpty(PhotonNetwork.NickName);

		public bool IsMasterClient => 
			PhotonNetwork.IsMasterClient;
		public string CurrentRoomName => 
			PhotonNetwork.CurrentRoom.Name;
		public IEnumerable<string> PlayersNickNames => 
			PhotonNetwork.PlayerList.Select(k => k.NickName);
		public UnityAction OnJoinedLobbyAction { get; set; }
		public UnityAction OnJoinedRoomAction { get; set; }
		public UnityAction<string> OnPlayerEnteredRoomAction { get; set; }
		public UnityAction OnPlayerLeftRoomAction { get; set; }
		public UnityAction<short, string> OnCreateRoomFailedAction { get; set; }

		public void SetNickName(string nickName) => 
			PhotonNetwork.NickName = nickName;

		public void CreateRoom(string roomName)
		{
			var options = new RoomOptions();
			options.MaxPlayers = 3;
			PhotonNetwork.CreateRoom(roomName, options);
		}

		public void LeaveRoom() => 
			PhotonNetwork.LeaveRoom();

		public override void OnJoinedRoom() => 
			OnJoinedRoomAction?.Invoke();

		public override void OnJoinedLobby() => 
			OnJoinedLobbyAction?.Invoke();

		public override void OnPlayerEnteredRoom(Player newPlayer) => 
			OnPlayerEnteredRoomAction?.Invoke(newPlayer.NickName);

		public override void OnPlayerLeftRoom(Player otherPlayer) => 
			OnPlayerLeftRoomAction?.Invoke();

		public override void OnCreateRoomFailed(short returnCode, string message) => 
			OnCreateRoomFailedAction?.Invoke(returnCode, message);
	}
}