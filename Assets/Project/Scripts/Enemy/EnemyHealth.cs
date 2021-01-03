using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public FloatVariable health;
    [SerializeField] public FloatVariable playerAttack;
    [SerializeField] private GameEventListener _damageEvent;
    [SerializeField] private UnityEvent _deathEvent;
    private float _currentHealth;
    private int _id;

    private void Start()
    {
        _currentHealth = health.RuntimeValue;
        _id = transform.gameObject.GetInstanceID();
        _damageEvent.AddResponse(UpdateHealth);
    }

    public void UpdateHealth(int id)
    {
        if (_id == id)
        {
            _currentHealth -= playerAttack.RuntimeValue;
        
            if (_currentHealth <= 0)
            {
                gameObject.SetActive(false);
                _deathEvent.Invoke();
            }
        }
    }
}
