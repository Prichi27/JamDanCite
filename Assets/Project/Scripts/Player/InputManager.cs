using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] FloatVariable movementSpeed;
    [SerializeField] Vector2Variable _position;
    [SerializeField] BoolVariable _isDead;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Vector2 _movement;
    private Vector2 _mousePosition;

    private void Awake()
    {
        _position.RuntimeValue = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isDead.RuntimeValue) return;

        GetPlayerPosition();
        GetUserInput();
        _position.RuntimeValue = transform.position;
    }

    private void FixedUpdate()
    {
        if (_isDead.RuntimeValue) return;

        MovePlayer();
    }

    /// <summary>
    /// Get mouse input
    /// </summary>
    private void GetPlayerPosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = GetAngleFromPosition(_mousePosition, _rigidBody.position);

        // Skew angle by 45 deg to determine quadrant
        float skewedAngle = (angle + 45) * Mathf.Deg2Rad;

        if (Mathf.Sign(Mathf.Sin(skewedAngle)) == 1 && Mathf.Sign(Mathf.Cos(skewedAngle)) == 1 && Mathf.Sign(Mathf.Tan(skewedAngle)) == 1)
        {
            // Back
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 1);
        }
        else if (Mathf.Sign(Mathf.Sin(skewedAngle)) == 1)
        {
            // Left
            _animator.SetFloat("Horizontal", -1);
            _animator.SetFloat("Vertical", 0);
        }
        else if (Mathf.Sign(Mathf.Tan(skewedAngle)) == 1)
        {
            // Front
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", -1);
        }
        else if (Mathf.Sign(Mathf.Cos(skewedAngle)) == 1)
        {
            // Right
            _animator.SetFloat("Horizontal", 1);
            _animator.SetFloat("Vertical", 0);
        }
    }

    private float GetAngleFromPosition(Vector2 mouse, Vector2 player)
    {
        return Mathf.Atan2(player.y - mouse.y, player.x - mouse.x) * Mathf.Rad2Deg + 90f;
    }

    private void GetUserInput()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        _rigidBody.MovePosition(_rigidBody.position + _movement * movementSpeed.RuntimeValue * Time.fixedDeltaTime);
        _animator.SetBool("IsMoving", _movement.x != 0 || _movement.y != 0);

    }
}
