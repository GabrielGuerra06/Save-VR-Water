using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class ProgressBar : MonoBehaviour
{
    public GameObject progressBar;

    public int time;
    void Start()
    {
                AnimateBar();

    }
    

    public void AnimateBar()
    {
        LeanTween.scaleX(progressBar, 1, time);
    }
}
