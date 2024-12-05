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
                }
                else
                {
                    currentTree.isBeingWatered = false;
                }
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tree"))
            {
                GameManager.Instance.progressBar.transform.parent.gameObject.SetActive(true);
                currentTree = other.GetComponent<Tree>();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Tree"))
            {
                if (currentTree == other.GetComponent<Tree>())
                {
                    GameManager.Instance.progressBar.transform.parent.gameObject.SetActive(false);
                    currentTree.isBeingWatered = false;
                    currentTree = null;
                }
            }
        }
    }
}
