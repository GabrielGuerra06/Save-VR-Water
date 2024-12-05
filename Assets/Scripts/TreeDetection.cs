using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDetection : MonoBehaviour
{
    private GameObject[] trees;
    // Start is called before the first frame update
    void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject tree in trees) {
            Debug.Log("Tree detected: " + tree.name);
        }
    }


}
