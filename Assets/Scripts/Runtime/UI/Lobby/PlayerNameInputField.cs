using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MGJ.Runtime.UI.Lobby
{
	[RequireComponent(typeof(TMP_InputField))]
	public class PlayerNameInputField : MonoBehaviour
	{
		private const string DefaultName = "Player";

		private void Awake()
		{
			GetComponent<TMP_InputField>().onValueChanged.AddListener(ChangeName);
		}

		private void Start()
		{
			GetComponent<TMP_InputField>().text = DefaultName;
			PhotonNetwork.NickName = DefaultName;
		}

		private void ChangeName(string newName)
		{
			if (string.IsNullOrEmpty(newName))
				return;

			PhotonNetwork.NickName = newName;
		}
	}
}