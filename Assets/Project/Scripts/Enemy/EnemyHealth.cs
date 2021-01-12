using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public EnemyStats enemyStats;
    [SerializeField] public FloatVariable playerAttack;
    [SerializeField] public FloatVariable _flyingSpeed;
    [SerializeField] public FloatVariable _scaleSpeed;
    [SerializeField] public FloatVariable _eulerAngle;
    [SerializeField] public Vector2Variable playerPosition;
    [SerializeField] private GameEventListener _damageEvent;
    [SerializeField] private GameEvent _deathEvent;
    [SerializeField] private GameObjectPool[] _drops;

    private float _currentHealth;
    private int _id;
    private Vector2 _direction;
    private float _currentSpeed;

    private void Start()
    {
        _id = transform.gameObject.GetInstanceID();
        _damageEvent.AddResponse(UpdateHealth);
    }

    private void OnEnable() 
    {
        transform.localScale = Vector3.one;
        _currentHealth = enemyStats.Health;
        _currentSpeed = enemyStats.Speed;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    private void Update()
    {
        if(_currentHealth <= 0)
        {
            Death();
        }
    }

    public void UpdateHealth(int id, Power power, bool isIce)
    {
        if (_id == id)
        {
            _direction = (playerPosition.RuntimeValue - (Vector2)transform.position).normalized;
            BloodParticleSystemHandler.Instance.SpawnBlood(transform.position, new Vector3(-_direction.x, -_direction.y, 0));
            _currentHealth -= power.damage;
           
            if(_currentHealth <= 0)
            {
                RandomDrop();
            } 
        }
    }

    private void Death()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        transform.position -= (Vector3)_direction * _flyingSpeed.RuntimeValue * Time.deltaTime;
        transform.localScale += Vector3.one * _scaleSpeed.RuntimeValue * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, 0, _eulerAngle.RuntimeValue * Time.deltaTime * 360);

        BloodParticleSystemHandler.Instance.SpawnBlood(transform.position, new Vector3(_direction.x, _direction.y, 0));
        ChecksIfObjectOutOfCameraView();
    }

    void ChecksIfObjectOutOfCameraView()
    {
        if (!GetComponentInChildren<Renderer>().isVisible)
        {
            gameObject.SetActive(false);
            _deathEvent.Raise(enemyStats);
            _deathEvent.Raise();
        }
    }

    private void RandomDrop()
    {
        int index = Random.Range(0, _drops.Length + 10);
        if(index < _drops.Length)
        {
            _drops[index].GetPooledObject(transform.position, Quaternion.identity);
        }

    }
}
