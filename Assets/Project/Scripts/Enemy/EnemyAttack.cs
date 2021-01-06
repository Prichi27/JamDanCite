using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField]
    private EnemyStats _enemy;

    [SerializeField]
    private GameEvent OnPlayerDamaged;

    private EnemyAI _enemyAI;
    private Animator _anim;

    private float _nextAttack = 0f;
    private bool _canAttack = false;

    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CanAttackFromDistance();
        Attack();
    }

    private void Attack()
    {
        if (_canAttack && Time.time > _nextAttack)
        {
            // Play Attack Animation 
            if (_anim)
            {
                _anim.SetTrigger("Attack");
            }

            if (_enemy.IsLongRanged)
            {
                // Shoot
                Debug.Log("Shoot");
            }

            else
            {
                OnPlayerDamaged.Raise();
            }

            _nextAttack = Time.time + _enemy.AttackSpeed;
        }
    }

    private void CanAttackFromDistance()
    {
        if (_enemy.IsLongRanged) _canAttack = _enemyAI.CanEnemyAttack();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            _canAttack = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            _canAttack = false;
        }
    }
}
