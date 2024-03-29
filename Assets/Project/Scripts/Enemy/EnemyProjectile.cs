﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Vector2Variable _playerPostion;
    [SerializeField] EnemyStats _enemy;
    [SerializeField] float _speed;
    [SerializeField] private GameEvent _onPlayerDamaged;
    [SerializeField] private GameObjectPool _particlesPool;
    private Animator _animator;
    private bool _isAnimationOver;
    private bool _isPlayerDamaged;

    Vector2 _direction;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if(_speed != 0)
        {
            _direction = (_playerPostion.RuntimeValue - (Vector2)transform.position);
            gameObject.GetComponent<Rigidbody2D>().AddForce(_direction * _speed);
        }
        _isAnimationOver = false;
        _isPlayerDamaged = false;
    }

    private void OnDisable() 
    {
        if(_speed == 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        ChecksIfObjectOutOfCameraView();
        if(_speed == 0 && !_isAnimationOver) IsAnimationOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Constants.PLAYER_TAG) && !_isPlayerDamaged)
        {
            DamagePlayer();
            if(_speed != 0) gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag(Constants.PLAYER_TAG) && !_isPlayerDamaged) 
        {
            DamagePlayer();
        }
    }

    void ChecksIfObjectOutOfCameraView()
    {
        if (!GetComponent<Renderer>().isVisible) gameObject.SetActive(false);
    }

    public void IsAnimationOver()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
        {
            _isAnimationOver = true;
            Invoke("EnableCollider", 1.0f);
        }
    }

    private void DamagePlayer()
    {
        // Spawn Blood
        var dir = ((Vector2)transform.position - _playerPostion.RuntimeValue).normalized;
        BloodParticleSystemHandler.Instance.SpawnBlood(_playerPostion.RuntimeValue, new Vector3(-dir.x, -dir.y, 0));
        _onPlayerDamaged.Raise(_enemy);
        _onPlayerDamaged.Raise();
        _isPlayerDamaged = true;

    }

    void EnableCollider()
    {
        if(_particlesPool) _particlesPool.GetPooledObject(transform.position, Quaternion.identity);
        
        GetComponent<CircleCollider2D>().enabled = true;

        Invoke("DeactivateProjectile", 0.5f);
    }

    void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }
}
