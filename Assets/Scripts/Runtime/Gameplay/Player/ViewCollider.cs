using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Tags;

namespace MGJ.Runtime.Gameplay.Player
{
    public class ViewCollider : MonoBehaviour
    {
        private GameObject selectedObject { get; set; }
        public PlayerController controller;

        [SerializeField] private Tag[] tags;
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.HasTag(tags[0])) {
                selectedObject = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other) {
            
        }


    }
}
