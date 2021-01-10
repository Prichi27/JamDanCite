using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public EnemyStats enemyStats;
    [SerializeField] public FloatVariable playerAttack;
    [SerializeField] public Vector2Variable playerPosition;
    [SerializeField] private GameEventListener _damageEvent;
    [SerializeField] private GameEvent _deathEvent;
    private float _currentHealth;
    private int _id;
    private Rigidbody2D _rb;

    private void Start()
    {
        _id = transform.gameObject.GetInstanceID();
        _rb = GetComponent<Rigidbody2D>();
        _damageEvent.AddResponse(UpdateHealth);
    }

    private void OnEnable() 
    {
        _currentHealth = enemyStats.Health;
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
                _deathEvent.Raise(enemyStats);
                _deathEvent.Raise();
            }
        }
    }
}
