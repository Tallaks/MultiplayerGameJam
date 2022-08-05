using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Tags;
using Photon.Pun;
using Photon.Realtime;

namespace MGJ
{
    public class GameManager : MonoBehaviourPun
    {
        public static GameManager instance;

        [SerializeField] public Tag[] allTags; // Stores all ScriptableObject tags in scene

        public GameObject ship;

        private void Awake() {
            instance = this;
        }

        private void Start() {
            if(PhotonNetwork.IsMasterClient) {
                //photonView.RPC("SpawnShip", RpcTarget.AllBuffered);
                PhotonNetwork.InstantiateRoomObject("Ship", new Vector3(60, -11, 0), Quaternion.identity);
            }
        }
        [PunRPC]
        private void SpawnShip() {
            //PhotonNetwork.Instantiate("Ship", new Vector3(60, -11, 0), Quaternion.identity);
            //Instantiate(ship, new Vector3(60, -11, 0), Quaternion.identity);
        }
    }
}
