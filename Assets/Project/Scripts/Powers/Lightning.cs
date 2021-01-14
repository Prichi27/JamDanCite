using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Projectile
{
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
                AddExplosionForce(other);

                OnEnemyDamaged.Raise(other.GetInstanceID());
                OnEnemyDamaged.Raise(other.GetInstanceID(),power);
                OnEnemyDamaged.Raise();
            }
        }

        transform.position = _newPosition;

        HasSpawned = true;
    }

    public override void IsAnimationOver()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
        {
            isAnimationOver = true;
            DeactivateProjectile();
        }
    }
}