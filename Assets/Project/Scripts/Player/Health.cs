using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _health;

    public void UpdateHealth(EnemyStats enemy)
    {
        _health.RuntimeValue -= enemy.Damage;

        if (_health.RuntimeValue <= 0)
        {
            // The sucker dies! 
        }
    }
}
