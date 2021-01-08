using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private FloatVariable damage;
    [SerializeField] private Power power;
    private Animator _animator;
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public Vector2 offset = new Vector2(0.0f, 0.0f);
    [SerializeField] public GameEvent OnEnemyDamaged;

    private void Start() 
    {
        GetComponent<SpriteRenderer>().sprite = power.sprite;

        _animator = GetComponent<Animator>();
        _animator.keepAnimatorControllerStateOnDisable = true;
        _animator.SetTrigger(power.name);
    }

    void Update()
    {
        ChecksIfObjectOutOfCameraView();
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition + offset, newPosition + offset);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag(Constants.ENEMY_TAG))
            {
                //var enemyHealth = other.GetComponent<EnemyHealth>();
                //if (enemyHealth)
                //{
                //    enemyHealth.UpdateHealth(damage);
                //}
                OnEnemyDamaged.Raise(other.GetInstanceID());
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

        transform.position = newPosition;
    }

    void ChecksIfObjectOutOfCameraView()
    {
        if(!GetComponent<Renderer>().isVisible) gameObject.SetActive(false);
    }
}
