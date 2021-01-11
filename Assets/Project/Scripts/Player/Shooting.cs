using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Space]
    [Header("Character attributes:")]
    public float crosshairDistance = 0.75f;
    public float bulletForce = 10f;

    [Space]
    [Header("References:")]
    public List<GameObject> staffPositions;
    private Animator _animator;
    public Texture2D cursorTexture;

    [SerializeField]
    [Tooltip("Gets reference to gameobject pool")]
    private GameObjectPool _projectilePool;

    [SerializeField]
    private GameEvent _shootEvent;

    [SerializeField] BoolVariable _isDead;

    private Vector2 _direction;
    private GameObject _currentStaffPosition;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Awake() 
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead.RuntimeValue) return;

        SetStaffPosition();
        ProcessInputs();
        
        if (Input.GetButtonDown("Fire1"))
        {
            _shootEvent.Raise();
            Attack();
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
        
        GameObject power = _projectilePool.GetPooledObject(firepoint, Quaternion.identity);
        Projectile projectileScript = power.GetComponent<Projectile>();
        projectileScript.SetVelocity(shootingDirection * bulletForce);
        projectileScript.SetRotation(Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
    }

    void ProcessInputs()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetProjectilePool(GameObjectPool projectilePool)
    {
        _projectilePool = projectilePool;
    }

    private bool IsLightningPower(){ return _projectilePool.name.Equals("Lightning"); }
}