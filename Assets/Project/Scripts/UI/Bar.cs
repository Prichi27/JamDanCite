using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Bar : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private FloatVariable _value;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        if (_slider)
        {
            _slider.maxValue = _value.RuntimeValue;
            UpdateValue();
        }
    }

    public void UpdateValue()
    {
        _slider.value = _value.RuntimeValue;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}
