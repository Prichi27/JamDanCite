using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _health;

    [SerializeField]
    private GameEventListener _playerDamage;

    private void OnEnable()
    {
        _playerDamage.AddResponse(UpdateHealth);
    }

    public void UpdateHealth(EnemyStats enemy)
    {
        _health.RuntimeValue -= enemy.Damage;

        if (_health.RuntimeValue <= 0)
        {
            // The sucker dies! 
        }
    }
}
