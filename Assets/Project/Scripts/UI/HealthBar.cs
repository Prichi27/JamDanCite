using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private FloatVariable _health;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        if (_slider)
        {
            _slider.maxValue = _health.RuntimeValue;
            UpdateHealth();
        }
    }

    public void UpdateHealth()
    {
        _slider.value = _health.RuntimeValue;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}
