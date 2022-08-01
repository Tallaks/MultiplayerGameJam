using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Tags;


namespace MGJ
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] public Tag[] allTags; // Stores all ScriptableObject tags in scene

        private void Awake() {
            instance = this;
        }
    }
}
