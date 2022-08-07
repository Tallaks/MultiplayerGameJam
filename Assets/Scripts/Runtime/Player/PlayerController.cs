using Photon.Pun;
using UnityEngine;
using ToolBox.Tags;
using ToolBox;
using System.Collections;
using UnityEngine.UI;

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
        public bool isDriving = false;

        public ViewCollider viewCollider; // Manager for selected objects
        public float objectFollowSpeed; // How fast an object will travel to grabPoint
        public Transform grabPoint; // Point at which objects will travel to when picking up objects

        /* SHIP DRIVING */
        private Vector3 m_EulerAngleVelocity;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float shipAcceleration;

        private Rigidbody shipRB;
        private GameObject ship;

        public float maxSpeed = .12f;
        public float currentSpeed;

        private Transform driverPosition;
        private Transform navigatorPosition;
        private Quaternion deltaRotation;

        private bool isSitting = false;
        public bool isNavigating = false;

        public GameObject waitText;

        private void Start() {
            driverPosition = GameObject.Find("SteeringTransform").transform;
            navigatorPosition = GameObject.Find("TopSeatSpot").transform;
            waitText.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;

            cam = Camera.main;

            ship = GameObject.FindGameObjectWithTag("Ship");

            transform.SetParent(ship.transform, true);
            shipRB = ship.GetComponent<Rigidbody>();
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

                if (!isDriving) {
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

                    shipRB.velocity = Vector3.zero;
                    shipRB.angularVelocity = Vector3.zero;
                }
                else {
                    DriveShip();
                }

                // Locking and unlocking cursor
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Cursor.lockState == CursorLockMode.None) {
                    if (Input.GetMouseButtonDown(0)) {
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }

                if(viewCollider.selectedObject.name == "SteeringCollider") {
                    LightObject(GameObject.Find("Panel"), 15);
                    if (Input.GetKeyDown(KeyCode.E)) {
                        isDriving = !isDriving;
                        StartCoroutine(WaitText());
                    }
                }

                else if(viewCollider.selectedObject.name == "TopSeat" && Input.GetKeyDown(KeyCode.E)) {
                    //photonView.RPC("SetNavigator", RpcTarget.All);
                    isSitting = !isSitting;
                }

                if(isSitting) {
                    transform.position = navigatorPosition.position;
                }
            }
            // Grab object if right click is pressed
            if(Input.GetMouseButton(1) && viewCollider.selectedObject != null) {
                PickupObject();
            }
            // Drop object when right click is released
            if(Input.GetMouseButtonUp(1)) {
                viewCollider.selectedObject = null;
            }
            // Drive ship
            /*if(Input.GetKeyDown("2")) {
                isDriving = !isDriving;
            }*/
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
            // Calculate distance between grabPoint and current object grabbed
            float distance = Vector3.Distance(grabPoint.transform.position, viewCollider.selectedObject.transform.position);
            // Drop object if it is too far
            if (distance > 1.35f)
                viewCollider.selectedObject = null;
        }

        private void DriveShip() {
            transform.position = driverPosition.position;
            //shipRB.AddForce(-Input.GetAxisRaw("Vertical") * shipSpeed * ship.transform.right);
            if (Input.GetAxisRaw("Vertical") == 1) {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Input.GetAxisRaw("Vertical") * Time.deltaTime * shipAcceleration);
            }
            else if (Input.GetAxisRaw("Vertical") == -1) {
                currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed/3, -Input.GetAxisRaw("Vertical") * Time.deltaTime * shipAcceleration / 4);
            }

            //ship.transform.position += -currentSpeed * ship.transform.transform.right * Time.deltaTime;

            if (currentSpeed > 0.002f || currentSpeed < -0.002f) {
                m_EulerAngleVelocity = new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed * currentSpeed * 10, 0);
                deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
                //shipRB.MoveRotation(shipRB.rotation * deltaRotation);
            }
            else {
                shipRB.angularVelocity = Vector3.zero;
            }

            photonView.RPC("ShipMovement", RpcTarget.All, -currentSpeed * ship.transform.transform.right * Time.deltaTime, deltaRotation);
        }
        [PunRPC]
        private void ShipMovement(Vector3 moveVector, Quaternion rotate) {
            ship.transform.position += moveVector;
            shipRB.MoveRotation(shipRB.rotation * rotate);
        }

        private IEnumerator WaitText() {
            waitText.SetActive(true);
            yield return new WaitForSeconds(3);
            waitText.SetActive(false);
        }
    }
}
