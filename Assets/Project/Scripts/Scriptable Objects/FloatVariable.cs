using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Float", order = 20)]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    public float Value;

    private float _runtimeValue;

    public float RuntimeValue { get { return _runtimeValue; } set { _runtimeValue = value; } }

    public void OnAfterDeserialize()
    {
        _runtimeValue = Value;
    }

    public void OnBeforeSerialize() {}

    public void Reset()
    {
        _runtimeValue = Value;
    }
}
