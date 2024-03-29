﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _health;

    [SerializeField] BoolVariable _isDead;

    [SerializeField] GameEvent _onPlayerDeath;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void UpdateHealth(EnemyStats enemy)
    {
        _health.RuntimeValue -= enemy.Damage;

        if (_health.RuntimeValue <= 0)
        {
            // The sucker dies! 
            _anim.SetBool("IsDead", true);
            _isDead.RuntimeValue = true;
            _onPlayerDeath.Raise();
        }
    }
}
