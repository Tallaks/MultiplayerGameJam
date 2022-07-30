using MGJ.Runtime.Gameplay.Player;
using Photon.Pun;
using UnityEngine;

namespace MGJ.Runtime.Gameplay
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner instance;

        private void Awake() {
            instance = this;
        }

        public GameObject playerPrefab;
        private GameObject player;

        void Start() {
            if(PhotonNetwork.IsConnected) {
                SpawnPlayer();
            }
        }

        public void SpawnPlayer() {
            Transform spawnPoint = SpawnManager.instance.GetSpawnPoint();

            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
