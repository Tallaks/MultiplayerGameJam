using MGJ.Runtime.Infrastructure.DI;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public interface IConnection : IService
	{
		void Connect();
	}
}