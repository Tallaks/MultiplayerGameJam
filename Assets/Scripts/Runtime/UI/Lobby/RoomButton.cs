using MGJ.Runtime.Infrastructure.Services.Network;
using TMPro;
using UnityEngine;

namespace MGJ.Runtime.UI.Lobby
{
    public class RoomButton : MonoBehaviour
    {
        public TMP_Text buttonText;
        
        private RoomDecorator _info;
        private Mediator _mediator;

        private void Awake() => 
            _mediator = FindObjectOfType<Mediator>();

        public void SetButtonDetails(RoomDecorator inputInfo) 
        {
            _info = inputInfo;
            buttonText.text = _info.Name;
        }

       public void OpenRoom() => 
           _mediator.JoinRoom(_info);
    }
}
