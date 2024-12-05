using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float elapsedTime ;
    public float totalTime = 5.0f;
    // Update is called once per frame

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime() / 60);
        int seconds = Mathf.FloorToInt(remainingTime() % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(remainingTime() < 0)
        {
            GameManager.Instance.ShowGameStatus(false);
            Time.timeScale = 0;  
            GameManager.Instance.StartAfterDelay();
        }
        
    }

    public float remainingTime()
    {
        return totalTime - elapsedTime;
    }
}
