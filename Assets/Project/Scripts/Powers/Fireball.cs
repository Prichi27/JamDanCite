using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{    
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
                AddExplosionForce(other);
                
                OnEnemyDamaged.Raise(other.GetInstanceID());
                OnEnemyDamaged.Raise(other.GetInstanceID(), power, false);
                OnEnemyDamaged.Raise();

                ShowParticleOnDestroy();
                DeactivateProjectile();
                break;
            }
                
            if (other.CompareTag(Constants.OBSTACLE_TAG))
            {        
                ShowParticleOnDestroy();
                DeactivateProjectile();
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
