using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private string Value;

    private string _runtimeValue;

    public string RuntimeValue { get { return _runtimeValue; } set { _runtimeValue = value; } }

    public void OnAfterDeserialize()
    {
        _runtimeValue = Value;
    }

    public void OnBeforeSerialize() { }
}
