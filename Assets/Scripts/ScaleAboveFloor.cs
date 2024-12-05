using UnityEngine;

public class ScaleAboveFloor : MonoBehaviour
{
    public float scaleSpeed = 5f;  
    public float minHeight = 0f;  

    private Vector3 initialScale;
    private Vector3 initialPosition;

    void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            float newScaleY = transform.localScale.y + scaleSpeed * Time.deltaTime;

            transform.localScale = new Vector3(
                transform.localScale.x,
                newScaleY,
                transform.localScale.z
            );

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
            transform.localScale = initialScale;
            transform.position = initialPosition;
        }
    }
}