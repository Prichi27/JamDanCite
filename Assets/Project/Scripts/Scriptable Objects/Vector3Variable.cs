using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Vector3", order = 20)]
public class Vector3Variable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private Vector3 Value;

    private Vector3 _runtimeValue;

    public Vector3 RuntimeValue { get { return _runtimeValue; } set { _runtimeValue = value; } }

    public void OnAfterDeserialize()
    {
        _runtimeValue = Value;
    }

    public void OnBeforeSerialize() { }
}
