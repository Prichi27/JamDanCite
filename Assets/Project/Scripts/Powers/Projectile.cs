using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private FloatVariable damage;
    [SerializeField] protected Power power;
    private Animator _animator;
    protected Vector2 _velocity = new Vector2(0.0f, 0.0f);
    public Vector2 offset = new Vector2(0.0f, 0.0f);
    [SerializeField] public GameEvent OnEnemyDamaged;

    protected Vector2 _currentPosition;
    protected Vector2 _newPosition;

    protected bool HasSpawned;

    private void Start() 
    {
        GetComponent<SpriteRenderer>().sprite = power.sprite;

        _animator = GetComponent<Animator>();
        // _animator.keepAnimatorControllerStateOnDisable = true;
        // _animator.SetTrigger(power.name);
    }

    private void OnEnable() 
    {
        HasSpawned = false;
        Invoke("DeactivateProjectile", 3.0f);
    }

    void Update()
    {
        ChecksIfObjectOutOfCameraView();
        SetPosition();
        if(!HasSpawned) HitEnemies();        
    }

    public void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }

    void ChecksIfObjectOutOfCameraView()
    {
        if(!GetComponent<Renderer>().isVisible) gameObject.SetActive(false);
    }

    public abstract void SetPosition();
    
    public abstract void SetVelocity(Vector2 velocity);

    public abstract void SetRotation(float zAngle);

    public abstract void HitEnemies();
}
