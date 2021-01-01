using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public FloatVariable health;
    [SerializeField] private UnityEvent _deathEvent;
    private float _currentHealt;

    private void Start()
    {
        _currentHealt = health.RuntimeValue;
    }

    public void UpdateHealth(FloatVariable playerAttack)
    {
        _currentHealt -= playerAttack.RuntimeValue;
        
        if (_currentHealt <= 0)
        {
            Destroy(gameObject);
            _deathEvent.Invoke();
        }
    }
}
