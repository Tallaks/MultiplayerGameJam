using Photon.Pun;
using UnityEngine;

namespace MGJ.Runtime.Gameplay.Player
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        public Transform viewPoint;
        public float mouseSensitivity = 1f;
        private float verticalRotStore;
        private Vector2 mouseInput;

        public bool invertLook;

        public float moveSpeed = 5f;
        private Vector3 moveDir, movement;
        public float jumpForce = 7f, gravityMod = 2f;

        public CharacterController controller;

        private Camera cam;

        public Transform groundCheck;
        private bool isGrounded;
        public LayerMask groundLayers;

        public bool isOnline;

        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;

            cam = Camera.main;
        }

        private void Update() {
            // Only control your your character
            if (photonView.IsMine || !isOnline) {

                mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

                verticalRotStore += mouseInput.y;
                verticalRotStore = Mathf.Clamp(verticalRotStore, -80f, 80f);

                if (invertLook) {
                    viewPoint.rotation = Quaternion.Euler(verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
                }
                else {
                    viewPoint.rotation = Quaternion.Euler(-verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
                }

                moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

                float yVel = movement.y;
                movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized;
                movement.y = yVel;

                if (controller.isGrounded) {
                    movement.y = 0f;
                }

                isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, .25f, groundLayers);

                if (Input.GetButtonDown("Jump") && isGrounded) {
                    movement.y = jumpForce;
                }

                movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;

                controller.Move(movement * moveSpeed * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Cursor.lockState == CursorLockMode.None) {
                    if (Input.GetMouseButtonDown(0)) {
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
        }

        private void LateUpdate() {
            if (photonView.IsMine || !isOnline) {
                cam.transform.position = viewPoint.position;
                cam.transform.rotation = viewPoint.rotation;
            }
        }
    }
}
