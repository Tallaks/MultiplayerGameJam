using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MGJ
{
    public class ShipCollider : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private GameObject winScreen;

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.tag == "Wall") {
                Cursor.lockState = CursorLockMode.None;
                loseScreen.SetActive(true);
            }

            if (collision.gameObject.tag == "Winner") {
                Cursor.lockState = CursorLockMode.None;
                winScreen.SetActive(true);
            }
        }

        private void Start() {
            loseScreen = GameObject.Find("LoseScreen");
            winScreen = GameObject.Find("WinScreen");
            loseScreen.SetActive(false);
            winScreen.SetActive(false);
        }
    }
}
