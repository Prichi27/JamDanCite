using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Power power;
    [SerializeField] protected FloatVariable _explosionForce;
    [SerializeField] protected Vector2Variable _playerPosition;
    [SerializeField] public GameEvent OnEnemyDamaged;
    [SerializeField] public GameEvent OnPlayerAttack;
    [SerializeField] public GameEvent OnManaUsed;

    protected Animator _animator;
    protected Vector2 _velocity = new Vector2(0.0f, 0.0f);
    public Vector2 offset = new Vector2(0.0f, 0.0f);

    protected Vector2 _currentPosition;
    protected Vector2 _newPosition;

    protected bool HasSpawned;

    protected bool isAnimationOver;

    private void Awake() 
    {
        _animator = GetComponent<Animator>();
    }

    private void Start() 
    {
        GetComponent<SpriteRenderer>().sprite = power.sprite;
        // _animator.keepAnimatorControllerStateOnDisable = true;

        ShowParticleOnShoot();
    }

    private void OnEnable() 
    {
        // OnManaUsed.Raise(power);

        _animator.SetTrigger("Shoot");

        isAnimationOver = false;
        HasSpawned = false;

        Invoke("DeactivateProjectile", 3.0f);
    }

    private void OnDisable() 
    {
        CancelInvoke();
    }

    void Update()
    {
        ChecksIfObjectOutOfCameraView();
        SetPosition();
        if(!HasSpawned) HitEnemies();        
        if(!isAnimationOver) IsAnimationOver();
    }

    public void DeactivateProjectile()
    {        
        if (power.manaDrain > 0) OnPlayerAttack.Raise(power);        
        gameObject.SetActive(false);
    }

    void SetPosition()
    {        
        _currentPosition = transform.position;
        _newPosition = _currentPosition + _velocity * Time.deltaTime;
    }

    void ChecksIfObjectOutOfCameraView()
    {
        if(!GetComponent<Renderer>().isVisible) gameObject.SetActive(false);
    }

    protected void ShowParticleOnShoot()
    {
        if(power.particleOnShoot) power.particleOnShoot.GetPooledObject(transform.position, Quaternion.identity);
    }

    protected void ShowParticleOnDestroy()
    {
        if(power.particleOnDestroy) power.particleOnDestroy.GetPooledObject(transform.position, Quaternion.identity);
    }

    protected void AddExplosionForce(GameObject other)
    {                        
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        Vector2 force = (Vector2)other.transform.position - (Vector2)_playerPosition.RuntimeValue;
        rb.AddForce(force.normalized * _explosionForce.RuntimeValue, ForceMode2D.Impulse);
    }
    
    public abstract void SetVelocity(Vector2 velocity);

    public abstract void SetRotation(float zAngle);

    public abstract void HitEnemies();
    public abstract void IsAnimationOver();
}
