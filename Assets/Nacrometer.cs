using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nacrometer : MonoBehaviour
{
    public Slider slider;

    public void SetMeterMax(int value)
    {
        slider.maxValue = value;
        slider.value = 0;
    }

    public void SetMeter(int value)
    {
        slider.value = value;
    }
}
