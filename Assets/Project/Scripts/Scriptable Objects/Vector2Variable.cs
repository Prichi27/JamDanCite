using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Vector2", order = 20)]
public class Vector2Variable : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private Vector2 Value;

    private Vector2 _runtimeValue;

    public Vector2 RuntimeValue { get { return _runtimeValue; } set { _runtimeValue = value; } }

    public void OnAfterDeserialize()
    {
        _runtimeValue = Value;
    }

    public void OnBeforeSerialize() { }
}
