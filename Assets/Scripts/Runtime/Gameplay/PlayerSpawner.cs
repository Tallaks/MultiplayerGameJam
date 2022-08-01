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
            else {
                SpawnTestPlayer();
            }
        }

        public void SpawnPlayer() {
            Transform spawnPoint = SpawnManager.instance.GetSpawnPoint();

            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }

        // Used for spawning player not connected to the network
        public void SpawnTestPlayer() {
            Transform spawnPoint = SpawnManager.instance.GetSpawnPoint();

            player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
