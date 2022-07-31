using System.Collections.Generic;
using MGJ.Runtime.UI.Lobby;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace MGJ.Runtime.Network
{
    public class Launcher : MonoBehaviourPunCallbacks {
        public static Launcher instance;
        private void Awake() {
            instance = this;
        }

        public GameObject loadingScreen;
        public TMP_Text loadingText;

        public GameObject menuButtons;

        public GameObject createRoomScreen;
        public TMP_InputField roomNameInput;

        public GameObject roomScreen;
        public TMP_Text roomNameText, playerNameLabel;
        private List<TMP_Text> allPlayerNames = new List<TMP_Text>();

        public GameObject errorScreen;
        public TMP_Text errorText;

        public GameObject roomBrowserScreen;
        public RoomButton theRoomButton;
        private List<RoomButton> allRoomButtons = new List<RoomButton>();

        public GameObject nameInputScreen;
        public TMP_InputField nameInput;
        private bool hasSetNick;

        public string levelToPlay;
        public GameObject startButton;
        
        void CloseMenus() {
            loadingScreen.SetActive(false);
            menuButtons.SetActive(false);
            createRoomScreen.SetActive(false);
            roomScreen.SetActive(false);
            errorScreen.SetActive(false);
            roomBrowserScreen.SetActive(false);
            nameInputScreen.SetActive(false);
        }
        
        public void QuickJoin() {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;

            PhotonNetwork.CreateRoom("Test");
            CloseMenus();
            loadingText.text = "Creating Room";
            loadingScreen.SetActive(true);
        }
    }
}
