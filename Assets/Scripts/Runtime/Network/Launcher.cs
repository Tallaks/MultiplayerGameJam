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
        
        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            // Destroy all room buttons
            foreach (RoomButton rb in allRoomButtons) {
                Destroy(rb.gameObject);
            }
            allRoomButtons.Clear();

            theRoomButton.gameObject.SetActive(false);

            // Create room button if room is not full and the room exists
            for (int i = 0; i < roomList.Count; i++) {
                if (roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList) {
                    RoomButton newButton = Instantiate(theRoomButton, theRoomButton.transform.parent);
                    newButton.SetButtonDetails(roomList[i]);
                    newButton.gameObject.SetActive(true);

                    allRoomButtons.Add(newButton);
                }
            }
        }

        public void JoinRoom(RoomInfo inputInfo) {
            PhotonNetwork.JoinRoom(inputInfo.Name);

            CloseMenus();
            loadingText.text = "Joining Room";
            loadingScreen.SetActive(true);
        }
        
        public override void OnMasterClientSwitched(Player newMasterClient) {
            // Only show start button for host
            if (PhotonNetwork.IsMasterClient) {
                startButton.SetActive(true);
            }
            else {
                startButton.SetActive(false);
            }
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
