using MGJ.Runtime.Gameplay.Player;
using Photon.Pun;
using UnityEngine;
using System.Collections;

namespace MGJ.Runtime.Gameplay
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner instance;

        private void Awake() {
            instance = this;
        }

        public GameObject playerPrefab;
        public GameObject loadingPanel;
        private GameObject player;

        void Start() {
            if(PhotonNetwork.IsConnected) {
                StartCoroutine(SpawnWait());
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

        public IEnumerator SpawnWait() {
            loadingPanel.SetActive(true);
            yield return new WaitForSeconds(1);
            loadingPanel.SetActive(false);
            SpawnPlayer();
        }
    }
}
