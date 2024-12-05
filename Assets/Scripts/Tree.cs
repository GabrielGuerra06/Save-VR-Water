using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public enum TreeState
    {
        NotWatered,
        Watered,
        OverWatered
    }

    public TreeState CurrentState { get; private set; } = TreeState.NotWatered;

    public bool isBeingWatered;
    public float wateringTime = 0;
    public float scaleSpeed = 1f;
    public float minHeight = 0f; 
    private Vector3 initialScale;
    private Vector3 initialPosition;
    [SerializeField] private float idealWateringTime = 10.0f;

    [Header("Indicator Prefabs")] 
    [SerializeField] private GameObject notWateredIndicatorPrefab;
    [SerializeField] private GameObject wateredIndicatorPrefab;
    [SerializeField] private GameObject overWateredIndicatorPrefab;

    private GameObject currentIndicator;
    [SerializeField] private float indicatorYPosition = 0.05f; // Y position for all indicators
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        UpdateTreeVisual();

    }

    private void Update()
    {
        if (isBeingWatered)
        {
            wateringTime += Time.deltaTime;
            float waterPercentage = wateringTime / idealWateringTime;

            // Update the progress bar scale
            Vector3 currentScale = GameManager.Instance.progressBar.gameObject.transform.localScale;
            currentScale.x = waterPercentage;
            GameManager.Instance.progressBar.gameObject.transform.localScale = currentScale;

            UpdateState();
            UpdateTreeVisual();
        }
    }

    private void UpdateState()
    {
        if (wateringTime < idealWateringTime)
        {
            CurrentState = TreeState.NotWatered;
        }
        else if (wateringTime >= idealWateringTime * 0.8f && wateringTime <= idealWateringTime * 1.2f)
        {
            CurrentState = TreeState.Watered;
        }
        else
        {
            CurrentState = TreeState.OverWatered;
        }

        Debug.Log($"Tree {gameObject.name} state updated to: {CurrentState}");
    }

    private void UpdateTreeVisual()
    {
        // Destroy the existing indicator
        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
        }

        // Determine the prefab to instantiate
        GameObject prefabToInstantiate = null;
        Vector3 indicatorPosition = Vector3.zero;
        Vector3 vectToPlayer = player.transform.position - transform.position;
        switch (CurrentState)
        {
            case TreeState.NotWatered:
                prefabToInstantiate = notWateredIndicatorPrefab;
                 indicatorPosition = new Vector3(this.transform.position.x -0.7f, this.transform.position.y
                    - 10, this.transform.position.z - 11.4f);
                break;
            case TreeState.Watered:
                indicatorPosition = this.transform.position + vectToPlayer*0.5f;
                prefabToInstantiate = wateredIndicatorPrefab;
                break;
            case TreeState.OverWatered:
                indicatorPosition = this.transform.position + vectToPlayer*0.5f;
                prefabToInstantiate = overWateredIndicatorPrefab;
                break;
        }

        // Instantiate the indicator at the correct position
        if (prefabToInstantiate != null)
        {

            currentIndicator = Instantiate(prefabToInstantiate, indicatorPosition, Quaternion.identity, transform);

            // Debug log for position
            Debug.Log($"Instantiated {CurrentState} indicator at position {indicatorPosition}");
        }
    }

    public void ResetTree()
    {
        CurrentState = TreeState.NotWatered;
        wateringTime = 0;
        UpdateTreeVisual();
    }
}
