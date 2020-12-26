using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float crosshairDistance = 0.6f;

    public GameObject bulletPrefab;
    public GameObject crosshair;

    private Vector3 _mousePosition;
    private Vector2 _direction;

    private Animator _animator;

    public float bulletForce = 20f;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Awake() 
    {
        Cursor.lockState = CursorLockMode.Confined;
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
        GameObject power = Instantiate(bulletPrefab, crosshair.transform.position, crosshair.transform.rotation);
        Rigidbody2D rigidBody = power.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(_direction * bulletForce, ForceMode2D.Impulse);
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
