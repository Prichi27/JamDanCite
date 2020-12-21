using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private FloatVariable _attackSpeed;
    [SerializeField] private FloatVariable _attackPoints;
    [SerializeField] private GameEvent OnPlayerDamaged;

    private float _nextAttack = 0f;
    private bool _canAttack = false;

    void Start()
    {        
        //StartCoroutine("AttackPlayer");
    }

    private void Update() 
    {
        if(_canAttack && Time.time > _nextAttack)
        {
            OnPlayerDamaged.Raise();
            _nextAttack = Time.time + _attackSpeed.RuntimeValue; 
        }
    }

    // private IEnumerator AttackPlayer()
    // {
    //     while(true)
    //     {            
    //         if(_rigidbody.IsTouchingLayers(LayerMask.GetMask("Player")))
    //         {
    //             OnPlayerDamaged.Raise();
    //         }
    //         yield return new WaitForSeconds(_attackSpeed.RuntimeValue);
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            _canAttack = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player"))
        {
            _canAttack = false;
        }
    }
}
