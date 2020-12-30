using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public FloatVariable health;

    public void UpdateHealth(FloatVariable playerAttack)
    {
        health.RuntimeValue -= playerAttack.RuntimeValue;
        Debug.LogWarning(health.RuntimeValue);
    }
}
