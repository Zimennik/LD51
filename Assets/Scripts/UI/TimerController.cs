using System.Collections;
using System.Collections.Generic;
using MPUIKIT;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private RectTransform _holder;
    [SerializeField] private MPImage _fillImage;

    public void SetTime(int time, int maxTime)
    {
        // Set the fill amount of the image
        //max time is 0
        //min time is 100

        _fillImage.fillAmount = 1 - ((float)time / (float)maxTime);
    }
}