using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Space]
    [Header("Character attributes:")]
    public float crosshairDistance = 0.75f;
    public float bulletForce = 10f;
    public FloatVariable _playerMP;

    [Space]
    [Header("References:")]
    public List<GameObject> staffPositions;
    private Animator _animator;
    public Texture2D cursorTexture;

    private GameObjectPool _currentPool;

    [SerializeField]
    [Tooltip("Gets reference to gameobject pool")]
    private GameObjectPool _defaultPool;

    [SerializeField]
    private GameEvent _shootEvent;

    [SerializeField] BoolVariable _isDead;

    [SerializeField] float _coolDown;

    private Vector2 _direction;
    private GameObject _currentStaffPosition;
    private float _nextAttack = 0;


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        SetDefaultPool();
    }

    private void Awake() 
    {
        #if UNITY_WEBGL
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        #else
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead.RuntimeValue) return;

        SetStaffPosition();
        ProcessInputs();

        if (Input.GetButtonDown("Fire1") && Time.time > _nextAttack )
        {
            _shootEvent.Raise();
            Attack();
            _nextAttack = Time.time + _coolDown;
            
        }
    }

    // Sets staff position
    void SetStaffPosition()
    {
        float horizontal = _animator.GetFloat("Horizontal");
        if (horizontal < 0) _currentStaffPosition = staffPositions.Find(e => e.name == Constants.LEFT_STAFF); 
        else _currentStaffPosition = staffPositions.Find(e => e.name == Constants.RIGHT_STAFF); 
    }

    void Attack()
    {
        Vector2 shootingDirection = _direction - (Vector2)transform.position;
        shootingDirection.Normalize();

        Vector2 firepoint = IsLightningPower() ? new Vector2(_direction.x, _direction.y) : new Vector2(_currentStaffPosition.transform.position.x, _currentStaffPosition.transform.position.y);
        
        GameObject power = _currentPool.GetPooledObject(firepoint, Quaternion.identity);
        Projectile projectileScript = power.GetComponent<Projectile>();
        projectileScript.SetVelocity(shootingDirection * bulletForce);
        projectileScript.SetRotation(Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
    }

    void ProcessInputs()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetProjectilePool(Pickup pickup)
    {
        _currentPool = pickup.projectilePool;
    }

    public void SetDefaultPool()
    {
        _currentPool = _defaultPool;
    }

    private bool IsLightningPower(){ return _currentPool.name.Equals("Lightning"); }    
}