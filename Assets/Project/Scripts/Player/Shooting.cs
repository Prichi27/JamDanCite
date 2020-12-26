using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float crosshairDistance = 2.0f;

    public List<GameObject> staffPositions;
    public GameObject bulletPrefab;
    public GameObject crosshair;

    private GameObject _currentStaffPosition;
    private Vector3 _mousePosition;
    private Vector3 _direction;

    private Animator _animator;

    public float bulletForce = 20f;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        SetStaffPosition();
        ProcessInputs();
        Aim();

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log(_direction);
        GameObject power = Instantiate(bulletPrefab, _currentStaffPosition.transform.position, _currentStaffPosition.transform.rotation);
        Rigidbody2D rigidBody = power.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(_direction * bulletForce, ForceMode2D.Impulse);
    }

    // Sets staff position
    void SetStaffPosition()
    {
        float horizontal = _animator.GetFloat("Horizontal");
        if (horizontal < 0) _currentStaffPosition = staffPositions.Find(e => e.name == Constants.LEFT_STAFF); 
        else _currentStaffPosition = staffPositions.Find(e => e.name == Constants.RIGHT_STAFF); 
    }

    void ProcessInputs()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (_mousePosition - _currentStaffPosition.transform.position);
        _direction.Normalize();
    }

    void Aim()
    {
        if (_direction != Vector3.zero)
        {
            crosshair.transform.localPosition = _direction * crosshairDistance;
        }
    }
}
