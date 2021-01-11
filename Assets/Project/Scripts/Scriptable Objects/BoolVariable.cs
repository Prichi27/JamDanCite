using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool", order = 20)]
public class BoolVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private bool Value;

    private bool _runtimeValue;

    public bool RuntimeValue { get { return _runtimeValue; } set { _runtimeValue = value; } }

    public void OnAfterDeserialize()
    {
        _runtimeValue = Value;
    }

    public void OnBeforeSerialize() { }
}
