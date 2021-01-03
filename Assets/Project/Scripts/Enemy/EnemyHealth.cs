using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public FloatVariable health;
    [SerializeField] private UnityEvent _deathEvent;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = health.RuntimeValue;
    }

    public void UpdateHealth(float playerAttack)
    {
        _currentHealth -= playerAttack;
        
        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            _deathEvent.Invoke();
        }
    }
}
