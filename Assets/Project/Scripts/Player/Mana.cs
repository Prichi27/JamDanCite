using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Mana : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _mana;
    
    public void UpdateMana(Power power)
    {
        _mana.RuntimeValue = power.manaDrain <= _mana.RuntimeValue ? _mana.RuntimeValue - power.manaDrain : 0;

        if (_mana.RuntimeValue <= 0)
        {
            GetComponent<Shooting>().SetDefaultPool();
        }
        Debug.LogError("Mana: " + _mana.RuntimeValue);
    }
}
