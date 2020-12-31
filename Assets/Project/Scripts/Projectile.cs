using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private FloatVariable damage;
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public Vector2 offset = new Vector2(0.0f, 0.0f);
    [SerializeField] public GameEvent OnEnemyDamaged;

    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag("Enemy"))
            {
                var enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth)
                {
                    enemyHealth.UpdateHealth(damage);
                }
                Destroy(gameObject);
                break;
            }
                
            if (other.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
                break;
            }
            
        }

        transform.position = newPosition;
    }
}
