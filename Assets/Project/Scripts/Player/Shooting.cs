using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Space]
    [Header("Character attributes:")]
    public float crosshairDistance = 0.6f;
    public float bulletForce = 10f;

    [Space]
    [Header("References:")]
    public GameObject crosshair;
    private Animator _animator;
    
    [Space]
    [Header("Prefabs:")]
    public GameObject bulletPrefab;
    
    private Vector3 _mousePosition;
    private Vector2 _direction;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Awake() 
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Aim();
                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        // GameObject power = Instantiate(bulletPrefab, crosshair.transform.position, crosshair.transform.rotation);
        // Rigidbody2D rigidBody = power.GetComponent<Rigidbody2D>();
        // rigidBody.AddForce(_direction * bulletForce, ForceMode2D.Impulse);
        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();

        GameObject power = Instantiate(bulletPrefab, crosshair.transform.position, Quaternion.identity);
        Projectile projectileScript = power.GetComponent<Projectile>();
        projectileScript.velocity = shootingDirection * bulletForce;
        power.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        Destroy(power, 2.0f);
    }

    void ProcessInputs()
    {
        _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction.Normalize();
    }

    void Aim()
    {
        if (_direction != Vector2.zero)
        {
            crosshair.transform.localPosition = _direction * crosshairDistance;
        }
    }
}
