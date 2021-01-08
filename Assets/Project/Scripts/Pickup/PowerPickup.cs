using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    [SerializeField] private Pickup _pickup;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _pickup.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            Shooting script = other.gameObject.GetComponent<Shooting>();
            script.SetProjectilePool(_pickup.projectilePool);
            gameObject.SetActive(false);
        }
    }
}
