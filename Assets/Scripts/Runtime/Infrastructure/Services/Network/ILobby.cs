using System.Collections.Generic;
using MGJ.Runtime.Infrastructure.DI;
using UnityEngine.Events;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public interface ILobby : IService
	{
		bool NickNameEntered { get; }
		bool IsMasterClient { get; }
		string CurrentRoomName { get; }
		IEnumerable<string> PlayersNickNames { get; }
		UnityAction OnJoinedLobbyAction { get; set; }
		UnityAction OnJoinedRoomAction { get; set; }
		UnityAction<string> OnPlayerEnteredRoomAction { get; set; }
		UnityAction OnPlayerLeftRoomAction { get; set; }
		UnityAction<short, string> OnCreateRoomFailedAction { get; set; }
		UnityAction OnLeftRoomAction { get; set; }
		UnityAction<IEnumerable<RoomDecorator>> OnRoomListUpdateAction { get; set; }
		UnityAction<PlayerDecorator> OnMasterClientSwitchedAction { get; set; }

		void SetNickName(string nickName);
		void CreateRoom(string roomName);
		void JoinRoom(RoomDecorator room);
		void LeaveRoom();
	}
}