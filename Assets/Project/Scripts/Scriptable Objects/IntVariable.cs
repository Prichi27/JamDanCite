using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int", order = 20)]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private int Value;

    private int _runtimeValue;

    public int RuntimeValue { get { return _runtimeValue; } set { _runtimeValue = value; } }

    public void OnAfterDeserialize()
    {
        _runtimeValue = Value;
    }

    public void OnBeforeSerialize() { }
}
