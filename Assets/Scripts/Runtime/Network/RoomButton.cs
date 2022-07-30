using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace MGJ.Runtime.Network
{
    public class RoomButton : MonoBehaviour
    {
        public TMP_Text buttonText;

        private RoomInfo info;

    
        public void SetButtonDetails(RoomInfo inputInfo) {
            info = inputInfo;

            buttonText.text = info.Name;
        }

        public void OpenRoom() {
            Launcher.instance.JoinRoom(info);
        }
    }
}
