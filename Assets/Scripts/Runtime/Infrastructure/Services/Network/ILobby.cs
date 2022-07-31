using MGJ.Runtime.Infrastructure.DI;
using UnityEngine.Events;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public interface ILobby : IService
	{
		bool NickNameEntered { get; }
		UnityAction OnJoinedLobbyAction { get; set; }
		void SetNickName(string nickName);
	}
}