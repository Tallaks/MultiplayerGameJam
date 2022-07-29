using System;
using MGJ.Runtime.UI.Lobby;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MGJ.Runtime.Infrastructure
{
	public class Lobby : MonoBehaviourPunCallbacks
	{
		[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
		[SerializeField]
		private byte _maxPlayersPerRoom = 2;

		[SerializeField] private LobbyMediator _mediator;

		private string GameVersion =>
			Application.version;

		private void Awake() => 
			PhotonNetwork.AutomaticallySyncScene = true;

		public override void OnConnectedToMaster()
		{
			Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
			PhotonNetwork.JoinRandomRoom();
		}

		public override void OnJoinedRoom() => 
			Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

		public override void OnJoinRandomFailed(short returnCode, string message)
		{
			Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
			PhotonNetwork.CreateRoom(null, new RoomOptions(){MaxPlayers = _maxPlayersPerRoom});
		}
		
		public override void OnDisconnected(DisconnectCause cause)
		{
			_mediator.OnDisconnected();
			Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}",
				cause);
		}

		public void Connect()
		{
			_mediator.OnConnect();
			if (PhotonNetwork.IsConnected)
			{
				PhotonNetwork.JoinRandomRoom();
			}
			else
			{
				PhotonNetwork.ConnectUsingSettings();
				PhotonNetwork.GameVersion = GameVersion;
			}
		}
	}
}