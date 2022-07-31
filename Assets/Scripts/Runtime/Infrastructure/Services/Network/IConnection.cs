using MGJ.Runtime.Infrastructure.DI;
using UnityEngine.Events;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public interface IConnection : IService
	{
		UnityAction OnConnectedAction { get; set; }
		void Connect();
	}
}