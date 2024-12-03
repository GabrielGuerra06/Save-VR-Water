using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualMovement : MonoBehaviour
{
    public float speed = 5f;
    public float cameraRotation = 100f;
    public Transform cameraTransform;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 movement =  Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right; 
        }
        if(Input.GetKey(KeyCode.S)) { 
            movement += Vector3.back;
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, cameraRotation * Time.deltaTime);
        }
    }
}
