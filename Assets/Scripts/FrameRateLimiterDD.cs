using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateLimiterDD : MonoBehaviour
{
    public Slider frameRateSlider;
    public Text frameRateText;
    void Start()
    {
        frameRateSlider = GetComponent<Slider>();
        frameRateSlider.value = Screen.currentResolution.refreshRate;
        SetFrameRateLimit();
        frameRateSlider.onValueChanged.AddListener(delegate { SetFrameRateLimit(); });
    }
    public void SetFrameRateLimit()
    {
        Application.targetFrameRate = (int)frameRateSlider.value;
        frameRateText.text = Application.targetFrameRate.ToString();
    }
}
