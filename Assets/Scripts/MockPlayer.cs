using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace 
{
    public class MockPlayer: MonoBehaviour
    {
        public float moveSpeed = 5f; 

        public int waterAmountPerUse = 5; 
        [SerializeField]
        private DynamicWater dynamicWater;

        private Tree currentTree;

        private void Update()
        {
            HandleWatering();
        }
        
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
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tree"))
            {
                GameManager.Instance.progressBar.transform.parent.gameObject.SetActive(true);
                currentTree = other.GetComponent<Tree>();
                Debug.Log("Entered tree trigger: " + currentTree.name);
            }
        }
        
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
