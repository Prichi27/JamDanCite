using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumberDisplay : MonoBehaviour
{
    [SerializeField] IntVariable _wave;
    [SerializeField] TextMeshProUGUI _text;

    private void Start()
    {
        Invoke("UpdateWaveNumber", 0.5f);
    }

    public void UpdateWaveNumber()
    {
        _text.text = $"WAVE\n{_wave.RuntimeValue}";
    }
}
