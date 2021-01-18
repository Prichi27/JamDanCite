using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField]
    private GameEvent _pickup;

    [SerializeField]
    private FloatVariable _value;

    [SerializeField]
    private FloatVariable _increaseBy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _value.RuntimeValue += _increaseBy.RuntimeValue;
            if (_value.RuntimeValue >= _value.Value) _value.RuntimeValue = _value.Value;
            _pickup.Raise();
            gameObject.SetActive(false);
        }
    }
}
