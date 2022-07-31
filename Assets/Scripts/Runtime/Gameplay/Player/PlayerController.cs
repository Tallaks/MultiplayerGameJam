using Photon.Pun;
using UnityEngine;
using ToolBox.Tags;
using ToolBox;

namespace MGJ.Runtime.Gameplay.Player
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        public Transform viewPoint;
        public float mouseSensitivity = 1f;
        private float verticalRotStore;
        private Vector2 mouseInput;

        public bool invertLook; // Invert mouse up and down movement

        public float moveSpeed = 5f;
        private Vector3 moveDir, movement;
        public float jumpForce = 7f, gravityMod = 2f;

        public CharacterController controller;

        private Camera cam;

        public Transform groundCheck; // Point where groundCheck raycast starts
        private bool isGrounded;
        public LayerMask groundLayers;

        public bool isOnline; // Boolean for testing. Leave ON for networking and game builds, OFF for testing without connection to the network.

        public ViewCollider viewCollider; // Manager for selected objects
        public float objectFollowSpeed; // How fast an object will travel to grabPoint
        public Transform grabPoint; // Point at which objects will travel to when picking up objects

        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;

            cam = Camera.main;
        }

        private void Update() {
            // Only control your your character
            if (photonView.IsMine || !isOnline) {

                mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
                // Rotate player around y axis
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

                verticalRotStore += mouseInput.y;
                verticalRotStore = Mathf.Clamp(verticalRotStore, -80f, 80f);
                // Moving camera up and down
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
                // Check if player is on the ground with raycast
                isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, .25f, groundLayers);

                if (Input.GetButtonDown("Jump") && isGrounded) {
                    movement.y = jumpForce;
                }

                movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;

                controller.Move(movement * moveSpeed * Time.deltaTime);

                // Locking and unlocking cursor
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Cursor.lockState == CursorLockMode.None) {
                    if (Input.GetMouseButtonDown(0)) {
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
            // Grab object if right click is pressed
            if(Input.GetMouseButton(1) && viewCollider.selectedObject != null) {
                PickupObject();
            }
        }

        private void LateUpdate() {
            if (photonView.IsMine || !isOnline) {
                cam.transform.position = viewPoint.position;
                cam.transform.rotation = viewPoint.rotation;
            }
        }

        // Outline object with specified outline width
        public void LightObject(GameObject lightableObject, int outlineWidth) {
            lightableObject.GetComponent<Outline>().OutlineWidth = outlineWidth;
        }

        // Checks if object can be picked up, then moves the selected object to the grab point.
        private void PickupObject() {
            if(viewCollider.selectedObject.tag == "Tagable" && viewCollider.selectedObject.GetComponent<Rigidbody>() != null) {
                if (viewCollider.selectedObject.HasTag(GameManager.instance.allTags[1])) {
                    viewCollider.selectedObject.GetComponent<Rigidbody>().velocity = ((grabPoint.transform.position - viewCollider.selectedObject.transform.position) * objectFollowSpeed / viewCollider.selectedObject.GetComponent<Rigidbody>().mass);
                }
            }
            // Calculate distance to let go of object when the object is too far from the grabpoint
            float distance = Vector3.Distance(grabPoint.transform.position, viewCollider.selectedObject.transform.position);

            if (distance > 1.35f)
                viewCollider.selectedObject = null;
        }
    }
}
