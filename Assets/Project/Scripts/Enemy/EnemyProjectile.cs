using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Vector2Variable _playerPostion;
    [SerializeField] EnemyStats _enemy;
    [SerializeField] float _speed;
    [SerializeField] private GameEvent _onPlayerDamaged;

    Vector2 _direction;

    private void OnEnable()
    {
        _direction = (_playerPostion.RuntimeValue - (Vector2)transform.position);
        gameObject.GetComponent<Rigidbody2D>().AddForce(_direction * _speed);
    }

    private void Update()
    {
        ChecksIfObjectOutOfCameraView();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _onPlayerDamaged.Raise(_enemy);
            _onPlayerDamaged.Raise();
            gameObject.SetActive(false);
        }
    }

    void ChecksIfObjectOutOfCameraView()
    {
        if (!GetComponent<Renderer>().isVisible) gameObject.SetActive(false);
    }
}
