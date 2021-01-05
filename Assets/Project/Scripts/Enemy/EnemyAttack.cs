using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField]
    private EnemyStats _enemy;

    [SerializeField]
    private GameEvent OnPlayerDamaged;

    private float _nextAttack = 0f;
    private bool _canAttack = false;

    private void Update() 
    {
        if(_canAttack && Time.time > _nextAttack)
        {
            OnPlayerDamaged.Raise();
            _nextAttack = Time.time + _enemy.AttackSpeed; 
        }
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
