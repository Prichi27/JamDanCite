﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public FloatVariable health;
    [SerializeField] public FloatVariable playerAttack;
    [SerializeField] public Vector2Variable playerPosition;
    [SerializeField] private GameEventListener _damageEvent;
    [SerializeField] private UnityEvent _deathEvent;
    private float _currentHealth;
    private int _id;
    private Rigidbody2D _rb;

    private void Start()
    {
        _currentHealth = health.RuntimeValue;
        _id = transform.gameObject.GetInstanceID();
        _rb = GetComponent<Rigidbody2D>();
        _damageEvent.AddResponse(UpdateHealth);
    }

    public void UpdateHealth(int id)
    {
        if (_id == id)
        {
            var dir = (playerPosition.RuntimeValue - (Vector2)transform.position).normalized;
            BloodParticleSystemHandler.Instance.SpawnBlood(transform.position, new Vector3(-dir.x, -dir.y, 0));
            _currentHealth -= playerAttack.RuntimeValue;

            if (_currentHealth <= 0)
            {
                gameObject.SetActive(false);
                _deathEvent.Invoke();
            }
        }
    }
}
