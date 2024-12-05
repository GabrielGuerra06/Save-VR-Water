using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DynamicWater : MonoBehaviour
{
    public ParticleSystem waterParticleSystem;
    public Transform rightHandAnchor;
    public bool isWatering;
    public float particlesPerDegree = 10f;
    
    [SerializeField] private AudioSource wateringAudioSource; 
    [SerializeField] private AudioClip wateringMusicClip;
    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
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

        emissionModule = waterParticleSystem.emission;
    }

    void Update()
    {
        if (rightHandAnchor == null)
        {
            return;
        }

        float angleZ = rightHandAnchor.eulerAngles.z;
        if (angleZ > 180f)
        {
            angleZ -= 360f;
        }
        angleZ = Mathf.Clamp(angleZ, 0f, 90f);

        float emissionRate = angleZ * particlesPerDegree;
        emissionRate = Mathf.Max(emissionRate, 0f);

        var rateOverTime = emissionModule.rateOverTime;
        rateOverTime.constant = emissionRate;
        emissionModule.rateOverTime = rateOverTime;
        if (emissionRate > 0)
        {
            isWatering = true;
            GameManager.Instance.waterUsed += Time.deltaTime;
            if (!wateringAudioSource.isPlaying)
            {
                wateringAudioSource.clip = wateringMusicClip;
                wateringAudioSource.Play();
            }
            
        }
        else
        {
            isWatering = false;
            wateringAudioSource.Stop();
        }
    }
}
