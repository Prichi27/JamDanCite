using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Projectile
{
    [SerializeField] private FloatVariable _explosionForce;
    [SerializeField] private Vector2Variable _playerPosition;

    public override void SetPosition()
    {
        _currentPosition = transform.position;
        _newPosition = _currentPosition + _velocity * Time.deltaTime;
    }

    public override void SetVelocity(Vector2 velocity)
    {
        return;
    }
    public override void SetRotation(float zAngle)
    {
        return;
    }

    public override void HitEnemies()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, power.damageRadius);

        foreach (Collider2D hit in colliders)
        {
            GameObject other = hit.gameObject;

            if (other.CompareTag(Constants.ENEMY_TAG))
            {                
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                Vector2 force = (Vector2)other.transform.position - (Vector2)_playerPosition.RuntimeValue;
                rb.AddForce(force.normalized * _explosionForce.RuntimeValue, ForceMode2D.Impulse);
                OnEnemyDamaged.Raise(other.GetInstanceID());
                OnEnemyDamaged.Raise(other.GetInstanceID(),power);
                OnEnemyDamaged.Raise();
            }
        }

        transform.position = _newPosition;

        HasSpawned = true;
    }
}