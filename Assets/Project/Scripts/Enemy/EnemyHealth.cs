using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public FloatVariable health;
    private float _currentHealt;

    private void Start()
    {
        _currentHealt = health.RuntimeValue;
    }

    public void UpdateHealth(FloatVariable playerAttack)
    {
        _currentHealt -= playerAttack.RuntimeValue;
        Debug.LogWarning(_currentHealt);

        if (_currentHealt <= 0)
        {
            Destroy(gameObject);
        }
    }
}
