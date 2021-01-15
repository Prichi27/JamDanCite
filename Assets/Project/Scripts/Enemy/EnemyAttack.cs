using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField]
    private EnemyStats _enemy;

    [SerializeField]
    private GameEvent OnPlayerDamaged;

    [SerializeField]
    private Vector2Variable playerPosition;

    [SerializeField]
    private GameObjectPool projectile;

    [SerializeField]
    private BoolVariable _isDead;

    private EnemyAI _enemyAI;
    private Animator _anim;
    private int _id;

    private float _nextAttack = 0f;
    private Rigidbody2D _rb;
    private bool _canAttack = false;
    private bool _isFrozen = false;

    private void Start()
    {
        _id = transform.gameObject.GetInstanceID();
        _enemyAI = GetComponent<EnemyAI>();
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDead.RuntimeValue) return;

        CanAttackFromDistance();
        Attack();
    }

    private void Attack()
    {
        if (_canAttack && !_isFrozen && Time.time > _nextAttack)
        {
            // Play Attack Animation 
            if (_anim)
            {
                _anim.SetTrigger("Attack");
            }

            if (_enemy.IsLongRanged)
            {
                // Shoot
                //var currentPlayerPosition = new Vector2(playerPosition.RuntimeValue.x, playerPosition.RuntimeValue.y);
                AttackFromDistance();
            }

            else
            {
                // Spawn Blood
                var dir = ((Vector2)transform.position - playerPosition.RuntimeValue).normalized;
                BloodParticleSystemHandler.Instance.SpawnBlood(playerPosition.RuntimeValue, new Vector3(-dir.x, -dir.y, 0));
                OnPlayerDamaged.Raise(_enemy);
                OnPlayerDamaged.Raise();
            }

            _nextAttack = Time.time + _enemy.AttackSpeed;
        }
    }

    private void CanAttackFromDistance()
    {
        if (_enemy.IsLongRanged) _canAttack = _enemyAI.CanEnemyAttack();
    }

    private void AttackFromDistance()
    {
        if(_enemy.IsLongRanged && _enemy.canAttackAtPlayerPosition)
        {
            projectile.GetPooledObject(playerPosition.RuntimeValue, Quaternion.identity);
        }
        else
        {
            projectile.GetPooledObject(transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            _canAttack = true;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            _canAttack = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void Freeze(int id)
    {
        if(_id == id) _isFrozen = true;
    } 

    public void Unfreeze(int id)
    {
        if(_id == id) _isFrozen = false;
    }
}
