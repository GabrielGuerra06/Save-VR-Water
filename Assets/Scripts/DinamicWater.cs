using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DynamicWater : MonoBehaviour
{
    [Header("Particle System Settings")]
    [Tooltip("Particle System that emits water particles.")]
    public ParticleSystem waterParticleSystem;

    [Header("Hand Anchor Settings")]
    [Tooltip("Transform of the right hand anchor to detect Z-axis inclination.")]
    public Transform rightHandAnchor;

    [Header("Emission Rate Settings")] [Tooltip("Multiplier to determine emission rate based on Z angle.")]
    public bool isWatering;
    public float particlesPerDegree = 10f;

    // Cached reference to the emission module of the particle system
    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        // Validate and cache the Particle System
        if (waterParticleSystem == null)
        {
            waterParticleSystem = GetComponent<ParticleSystem>();
            if (waterParticleSystem == null)
            {
                Debug.LogError("DynamicWater: No ParticleSystem assigned or found on the GameObject.");
                this.enabled = false;
                return;
            }
        }

        // Cache the emission module for efficient access
        emissionModule = waterParticleSystem.emission;
    }

    void Update()
    {
        if (rightHandAnchor == null)
        {
            Debug.LogError("DynamicWater: Right Hand Anchor not assigned.");
            return;
        }

        // Get the Z rotation angle in degrees
        float angleZ = rightHandAnchor.eulerAngles.z;

        // Convert angle from [0, 360] to [-180, 180] for easier handling
        if (angleZ > 180f)
        {
            angleZ -= 360f;
        }

        // Optional: Clamp the angle to a specific range if needed
        // For example, limit to [0, 90] degrees to prevent excessive emission rates
        angleZ = Mathf.Clamp(angleZ, 0f, 90f);

        // Calculate the emission rate
        float emissionRate = angleZ * particlesPerDegree;

        // Ensure emission rate is non-negative
        emissionRate = Mathf.Max(emissionRate, 0f);

        // Apply the emission rate to the particle system
        var rateOverTime = emissionModule.rateOverTime;
        rateOverTime.constant = emissionRate;
        emissionModule.rateOverTime = rateOverTime;
        if (emissionRate > 0)
        {
            isWatering = true;
            GameManager.Instance.waterUsed += Time.deltaTime;
        }
        else
        {
            isWatering = false;
        }
    }
}
