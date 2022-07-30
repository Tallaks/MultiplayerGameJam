using UnityEngine;
using UnityEngine.UI;

namespace MGJ.Runtime.UI.MainMenu
{
	public class UI : MonoBehaviour
	{
		[SerializeField] private Button _enterLobbyButton;
		[SerializeField] private Button _exitGameButton;
		[SerializeField] private Mediator _mediator;

		private void OnEnable()
		{
			_enterLobbyButton.onClick.AddListener(_mediator.EnterLobby);
			_enterLobbyButton.onClick.AddListener(_mediator.QuitGame);
		}

		private void OnDisable()
		{
			_enterLobbyButton.onClick.RemoveAllListeners();
			_enterLobbyButton.onClick.RemoveAllListeners();
		}
	}
}