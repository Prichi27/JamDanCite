using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    [SerializeField] private Pickup _pickup;
    [SerializeField] private GameEvent _powerPickupEvent;
    [SerializeField] private FloatVariable _despawnTime;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _pickup.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() 
    {
        Invoke("DeactivateObject", _despawnTime.RuntimeValue);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag(Constants.PLAYER_TAG))
        {
            _powerPickupEvent.Raise(_pickup);
            gameObject.SetActive(false);
        }
    }
}
