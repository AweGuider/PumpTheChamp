using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SleepBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    float sliderMax;
    [SerializeField]
    float sliderMin;
    [SerializeField]
    float sleepDecreaseAmount;
    [SerializeField]
    float sleepDecreaseDelay;

    public UnityEvent Sleep;

    void Start()
    {
        if (slider == null) Debug.LogError($"Slider is NOT set!");
        slider.maxValue = sliderMax;
        slider.minValue = sliderMin;
        slider.value = slider.maxValue;
    }

    private void FixedUpdate()
    {
        Invoke(nameof(DecreaseSleep), sleepDecreaseDelay);
    }

    private void DecreaseSleep()
    {
        if (slider.value == sliderMin)
            return;
        slider.value -= sleepDecreaseAmount;
    }

    public void OnSleep()
    {
        Sleep?.Invoke();

        Invoke(nameof(RestoreSleep), 1f);
    }

    private void RestoreSleep()
    {
        // Ideally it should add random amount / calculated amount, not fully restore
        slider.value = sliderMax;
    }

    private void OnValidate()
    {
        if (slider == null) slider = GetComponent<Slider>();
    }
}
