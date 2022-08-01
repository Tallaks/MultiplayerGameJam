using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Tags;

namespace MGJ.Runtime.Gameplay.Player
{
    public class ViewCollider : MonoBehaviour
    {
        public GameObject selectedObject;
        public PlayerController controller;


        // Highlight object and set selected object when viewCollider hits an object
        private void OnTriggerEnter(Collider other) {
            selectedObject = other.gameObject;
            if (other.gameObject.HasTag(GameManager.instance.allTags[0])) {
                controller.LightObject(other.gameObject, 15);
            }
        }

        private void OnTriggerStay(Collider other) {
            // Allow the player to grab the same object twice in a row.
            if(Input.GetMouseButton(1) && selectedObject == null && other.gameObject.HasTag(GameManager.instance.allTags[1])) {
                selectedObject = other.gameObject;
            }
        }

        // Unhighlight object
        private void OnTriggerExit(Collider other) {
            if (other.gameObject.HasTag(GameManager.instance.allTags[0])) {
                controller.LightObject(other.gameObject, 0);
            }
        }
    }
}
