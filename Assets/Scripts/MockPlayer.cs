using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace 
{
    public class MockPlayer: MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f; // Movement speed

        [Header("Watering Settings")]
        public int waterAmountPerUse = 5; // Amount of water applied per use
        [SerializeField]
        private DynamicWater dynamicWater;

        // Reference to the current tree being interacted with
        private Tree currentTree;

        private void Update()
        {
            HandleWatering();
        }

        /// <summary>
        /// Handles watering when the player is within a tree's trigger collider.
        /// </summary>
        private void HandleWatering()
        {
            if (currentTree != null)
            {
                if (dynamicWater.isWatering)
                {
                    currentTree.isBeingWatered = true;
                    Debug.Log("Watering " + currentTree.name);
                }
                else
                {
                    currentTree.isBeingWatered = false;
                    Debug.Log("Not watering " + currentTree.name);
                }
            }
        }

        /// <summary>
        /// Called when the player enters a trigger collider.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tree"))
            {
                GameManager.Instance.progressBar.transform.parent.gameObject.SetActive(true);
                currentTree = other.GetComponent<Tree>();
                Debug.Log("Entered tree trigger: " + currentTree.name);
            }
        }

        /// <summary>
        /// Called when the player exits a trigger collider.
        /// </summary>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Tree"))
            {
                if (currentTree == other.GetComponent<Tree>())
                {
                    GameManager.Instance.progressBar.transform.parent.gameObject.SetActive(false);
                    Debug.Log("Exited tree trigger: " + currentTree.name);
                    currentTree.isBeingWatered = false;
                    currentTree = null;
                }
            }
        }
    }
}
