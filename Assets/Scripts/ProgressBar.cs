using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class ProgressBar : MonoBehaviour
{
    public GameObject progressBar;

    public int time;
    // Start is called before the first frame update
    void Start()
    {
                AnimateBar();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(progressBar, 1, time);
    }
}
