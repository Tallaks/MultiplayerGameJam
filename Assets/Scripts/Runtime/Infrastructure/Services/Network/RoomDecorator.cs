using Photon.Realtime;

namespace MGJ.Runtime.Infrastructure.Services.Network
{
	public class RoomDecorator
	{ 
		public int PlayerCount { get; }
		public int MaxPlayers { get; }
		public bool RemovedFromList { get; }
		public string Name { get; }
		
		private RoomInfo _roomInfo;
		
		public RoomDecorator(RoomInfo roomInfo)
		{
			_roomInfo = roomInfo;
			PlayerCount = roomInfo.PlayerCount;
			MaxPlayers = roomInfo.MaxPlayers;
			RemovedFromList = roomInfo.RemovedFromList;
			Name = roomInfo.Name;
		}
	}
}