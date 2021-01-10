using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField]
    private GameEvent _pickup;

    [SerializeField]
    private FloatVariable _health;

    [SerializeField]
    private FloatVariable _increaseBy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _health.RuntimeValue += _increaseBy.RuntimeValue;
            if (_health.RuntimeValue >= _health.Value) _health.RuntimeValue = _health.Value;
            _pickup.Raise();
            gameObject.SetActive(false);
        }
    }
}
