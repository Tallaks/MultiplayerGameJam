using MGJ.Runtime.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MGJ.Runtime.UI.Lobby
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour
    {
        [FormerlySerializedAs("_launcher")] [SerializeField] private Infrastructure.Lobby _lobby;
            
        private void Awake() => 
            GetComponent<Button>().onClick.AddListener(_lobby.Connect);
    }
}
