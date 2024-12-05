using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DefaultNamespace; // Ensure you have this using directive to access Tree.TreeState

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        // Static instance of GameManager which allows it to be accessed by any other script
        public List<GameObject> correctlyWateredTrees = new List<GameObject>();
        public TextMeshProUGUI textMeshProComponent;
        public GameObject progressBar;
        public static GameManager Instance { get; private set; }

        public GameObject _book = null;

        public bool bookState = false;
        public OVRInput.Controller controller;

        public List<GameObject> trees = new List<GameObject>();
        public GameObject wateringCan;
        public float total_water = 10.0f;
        public float waterUsed = 0.0f;
        public float waterPercentage = 0.0f;
        private void Start()
        {
            // Find all GameObjects with the tag "tree"
            GameObject[] taggedTrees = GameObject.FindGameObjectsWithTag("Tree");

            // Add them to the list
            trees.AddRange(taggedTrees);

            // Optional: Debug to confirm
            Debug.Log("Number of trees added: " + trees.Count);
        }
        
        private void Update()
        {
            waterPercentage = waterUsed / total_water;

            textMeshProComponent.text = "" + GetTreeCountByState(Tree.TreeState.Watered);
            if(OVRInput.GetDown(OVRInput.RawButton.X))
            {
                wateringCan.SetActive(!wateringCan.activeSelf);
            }
        }

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            // If an instance already exists and it's not this one, destroy this GameObject
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Assign this instance to the static property
            Instance = this;

            // Make this GameObject persistent across scenes
            DontDestroyOnLoad(gameObject);

            // Initialize the GameManager
            InitializeGame();
        }

        // Example initialization method
        private void InitializeGame()
        {
            Debug.Log("Game Initialized.");
            // Add your initialization code here
        }

        // Example method
        public void StartGame()
        {
            Debug.Log("Game Started.");
            // Add your game start logic here
        }

        public void ToogleBook()
        {
            _book.SetActive(!_book.activeSelf);
        }
        
        public void forwardBook()
        {
            _book.GetComponent<book>().RotateForward();
        }
        
        public void backBook()
        {
            _book.GetComponent<book>().RotateBack();
        }
        

        /// <summary>
        /// Returns the number of trees in the specified TreeState.
        /// </summary>
        /// <param name="state">The TreeState to count.</param>
        /// <returns>The count of trees in the specified state.</returns>
        public int GetTreeCountByState(Tree.TreeState state)
        {
            int count = 0;

            foreach (GameObject treeObj in trees)
            {
                if (treeObj != null)
                {
                    Tree treeComponent = treeObj.GetComponent<Tree>();
                    if (treeComponent != null && treeComponent.CurrentState == state)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
