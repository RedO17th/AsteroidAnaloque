using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    public void Initialize(int amountHealth)
    {
        _healthSlider.maxValue = amountHealth;
    }

    public void SetHealth(int amountHealth)
    {
        _healthSlider.value = amountHealth;
    }
}
