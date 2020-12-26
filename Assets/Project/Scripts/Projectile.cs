using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            Debug.Log("Damage Enemy");
        }

        Debug.Log("Collision");
        Destroy(this);
    }
}
