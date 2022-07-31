using MGJ.Runtime.Infrastructure.DI;
using MGJ.Runtime.Infrastructure.Services.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MGJ.Runtime.UI.Lobby
{
	[RequireComponent(typeof(TMP_InputField))]
	public class LobbyInputField : MonoBehaviour
	{
		[SerializeField] private Mediator _mediator;
		[SerializeField] private Button _submitButton;
		
		private ILobby _lobbyService;
		
		private TMP_InputField _inputField;

		private void Awake()
		{
			Construct();

			_inputField = GetComponent<TMP_InputField>();
			
			DisplaySavedNick();
			_submitButton.onClick.AddListener(Submit);
		}

		private void Submit()
		{
			if(!string.IsNullOrEmpty(_inputField.text)) {
				_lobbyService.SetNickName(_inputField.text);

				PlayerPrefs.SetString("playerName", _inputField.text);
				
				_mediator.ShowMenu();
			}
		}

		private void DisplaySavedNick()
		{
			//_mediator.CloseAllMenus();
			if(PlayerPrefs.HasKey("playerName")) {
				_inputField.text = PlayerPrefs.GetString("playerName");
			}
		}

		private void Construct()
		{
			_lobbyService = Container.Services.Resolve<ILobby>();
		}
	}
}