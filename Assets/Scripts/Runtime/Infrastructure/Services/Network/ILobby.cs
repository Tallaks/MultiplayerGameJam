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
		void SetNickName(string nickName);
		void CreateRoom(string roomName);
	}
}