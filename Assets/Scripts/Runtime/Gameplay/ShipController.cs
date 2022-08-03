using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace MGJ.Runtime.Gameplay.Player {
    public class ShipController : MonoBehaviourPunCallbacks {

        [SerializeField] private Rigidbody rb;

        private Vector3 m_EulerAngleVelocity;

        public float rotateSpeed = 10f;
        public float moveSpeed = 10f;

        private void Update() {

        }

        private void Start() {
            /*foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
                if(go.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer) {
                    playerController = go.GetComponent<PlayerController>();
                }
            }*/
        }


        private void DriveShip() {

            m_EulerAngleVelocity = new Vector3(0, Input.GetAxisRaw("Horizontal") * rotateSpeed, 0);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);

            rb.AddForce(-Input.GetAxisRaw("Vertical") * moveSpeed, 0, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
