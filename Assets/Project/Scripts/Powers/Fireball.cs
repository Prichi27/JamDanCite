using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    public override void SetPosition()
    {
        _currentPosition = transform.position;
        _newPosition = _currentPosition + _velocity * Time.deltaTime;
    }
    
    public override void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }

    public override void SetRotation(float zAngle)
    {
        transform.Rotate(0, 0, zAngle);
    }
    
    public override void HitEnemies()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(_currentPosition + offset, _newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag(Constants.ENEMY_TAG))
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                Vector2 force = (Vector2)other.transform.position - (Vector2)_playerPosition.RuntimeValue;
                rb.AddForce(force.normalized * _explosionForce.RuntimeValue, ForceMode2D.Impulse);
                
                OnEnemyDamaged.Raise(other.GetInstanceID());
                OnEnemyDamaged.Raise(other.GetInstanceID(), power);
                OnEnemyDamaged.Raise();

                _particleSystemPool.GetPooledObject(transform.position, Quaternion.identity);

                gameObject.SetActive(false);
                break;
            }
                
            if (other.CompareTag(Constants.OBSTACLE_TAG))
            {        
                _particleSystemPool.GetPooledObject(transform.position, Quaternion.identity);

                gameObject.SetActive(false);
                break;
            }            
        }

        transform.position = _newPosition;
    }


    public override void IsAnimationOver()
    {
        return;
    }

}
