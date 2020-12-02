using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();


    }
    
    /// <summary>
    /// Get mouse input
    /// </summary>
    private void GetPlayerPosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = GetAngleFromPosition(mousePosition, _rigidBody.position);


        _rigidBody.rotation = angle;

        float skewedAngle = (angle + 45) * Mathf.Deg2Rad;

        // The suspense is killing me :(
        if (Mathf.Sign(Mathf.Sin(skewedAngle)) == 1 && Mathf.Sign(Mathf.Cos(skewedAngle)) == 1 && Mathf.Sign(Mathf.Tan(skewedAngle)) == 1)
        {
            // Back
            Debug.Log("All");
            //_spriteRenderer.sprite = Sprite 
        }
        else if (Mathf.Sign(Mathf.Sin(skewedAngle)) == 1)
        {
            // Left
            Debug.Log("Sin");
        }
        else if (Mathf.Sign(Mathf.Tan(skewedAngle)) == 1)
        {
            // Front
            Debug.Log("Tan");
        }
        else if (Mathf.Sign(Mathf.Cos(skewedAngle)) == 1)
        {
            // Right
            Debug.Log("Cos");
        }

        //Debug.Log(transform.eulerAngles.z);

    }

    private float GetAngleFromPosition(Vector2 mouse, Vector2 player)
    {
        return Mathf.Atan2(player.y - mouse.y, player.x - mouse.x) * Mathf.Rad2Deg + 90f;
    }

}
