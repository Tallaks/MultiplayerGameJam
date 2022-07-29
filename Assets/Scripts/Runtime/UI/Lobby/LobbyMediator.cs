using UnityEngine;

namespace MGJ.Runtime.UI.Lobby
{
	public class LobbyMediator : MonoBehaviour
	{
		[SerializeField] private GameObject _controlPanel;
		[SerializeField] private GameObject _progressLabel;

		private void Start() => 
			OnDisconnected();

		public void OnConnect()
		{
			_progressLabel.SetActive(true);
			_controlPanel.SetActive(false);
		}

		public void OnDisconnected()
		{
			_progressLabel.SetActive(false);
			_controlPanel.SetActive(true);
		}
	}
}