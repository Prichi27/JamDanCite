using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // cass nissa
    public void UpdateDisplayedUI(FloatVariable floatVariable)
    {
        floatVariable.RuntimeValue = -10;
        Debug.Log(floatVariable.RuntimeValue);
    }
}
