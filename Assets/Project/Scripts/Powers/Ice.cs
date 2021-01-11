using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Projectile
{
    [SerializeField] private GameEvent OnEnemyFreeze;
    public override void SetRotation(float zAngle)
    {
        transform.Rotate(0, 0, zAngle);
    }

    public override void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }

    public override void HitEnemies()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(_currentPosition + offset, _newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag(Constants.ENEMY_TAG))
            {
                
                OnEnemyDamaged.Raise(other.GetInstanceID());
                OnEnemyDamaged.Raise(other.GetInstanceID(), power, true);
                OnEnemyDamaged.Raise();

                gameObject.SetActive(false);
                break;
            }
                
            if (other.CompareTag(Constants.OBSTACLE_TAG))
            {        
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
