using UnityEngine;

public class ScaleAboveFloor : MonoBehaviour
{
    public float scaleSpeed = 5f;  // Speed of scaling
    public float minHeight = 0f;  // Minimum height for the bottom of the prefab

    private Vector3 initialScale;
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial scale and position of the prefab
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure interaction is with the Player
        {
            // Scale the prefab over time
            float newScaleY = transform.localScale.y + scaleSpeed * Time.deltaTime;

            // Update the scale
            transform.localScale = new Vector3(
                transform.localScale.x,
                newScaleY,
                transform.localScale.z
            );

            // Reposition the prefab to ensure it grows above the floor
            float heightOffset = (newScaleY - initialScale.y) / 2;
            transform.position = new Vector3(
                initialPosition.x,
                initialPosition.y + heightOffset,
                initialPosition.z
            );
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset scale and position (optional)
            transform.localScale = initialScale;
            transform.position = initialPosition;
        }
    }
}