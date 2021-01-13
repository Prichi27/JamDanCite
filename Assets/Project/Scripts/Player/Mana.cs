using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Mana : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _mana;
    [SerializeField] private GameEvent _powerPickup;
    
    public void ReduceMana(Power power)
    {
        _mana.RuntimeValue = power.manaDrain <= _mana.RuntimeValue ? _mana.RuntimeValue - power.manaDrain : 0;

        if (_mana.RuntimeValue <= 0)
        {
            GetComponent<Shooting>().SetDefaultPool();
        }
    }

    public void IncreaseMana(Pickup pickup)
    {        
        _mana.RuntimeValue += pickup.manaIncrease;
        if (_mana.RuntimeValue >= _mana.Value) _mana.RuntimeValue = _mana.Value;
        _powerPickup.Raise();
    }
}
