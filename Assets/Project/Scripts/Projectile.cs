using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public Vector2 offset = new Vector2(0.0f, 0.0f);
    public GameObject player;
    [SerializeField] public GameEvent OnEnemyDamaged;

    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        Debug.DrawLine(currentPosition, newPosition, Color.cyan);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if(other != player)
            {
                if (other.CompareTag("Enemy"))
                {
                    Destroy(gameObject);
                    OnEnemyDamaged.Raise();
                    Debug.Log(other.name);
                    break;
                }
                
                if (other.CompareTag("Obstacle"))
                {
                    Destroy(gameObject);
                    break;
                }


            }
        }

        transform.position = newPosition;
    }
}
