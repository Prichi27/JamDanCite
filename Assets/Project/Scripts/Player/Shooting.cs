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
    public GameObject crosshair;
    private Animator _animator;
    public Texture2D cursorTexture;
    
    [Space]
    [Header("Prefabs:")]
    public GameObject bulletPrefab;

    [SerializeField]
    [Tooltip("Gets reference to gameobject pool")]
    private GameObjectPool _projectilePool;

    [SerializeField]
    private GameEvent _shootEvent;

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
        Vector2 shootingDirection = _direction.normalized;
        
        GameObject power = _projectilePool.GetPooledObject(_currentStaffPosition.transform.position, Quaternion.identity);
        Projectile projectileScript = power.GetComponent<Projectile>();
        projectileScript.velocity = shootingDirection * bulletForce;
        power.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
    }

    void ProcessInputs()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    // void Aim()
    // {
    //     if (_direction != Vector2.zero)
    //     {
    //         crosshair.transform.localPosition = _direction.normalized * crosshairDistance;
    //     }
    // }
}
