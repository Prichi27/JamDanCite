using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    private GameObject _enemyParent;

    [SerializeField]
    private GameObject[] _enemies;

    [SerializeField]
    private IntVariable _waveEnemy;

    [SerializeField]
    private Vector2Variable _playerPosition;

    [SerializeField]
    private FloatVariable _spawnRadius;

    [SerializeField]
    private FloatVariable _offset;

    private Collider2D[] colliders;

    void Start()
    {
        _enemyParent = GameObject.Find(Constants.ENEMIES_PARENT);
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < _waveEnemy.RuntimeValue; i++)
        {
            Instantiate(_enemies[Random.Range(0, _enemies.Length)], SetSpawnPosition(), Quaternion.identity, _enemyParent.transform);
        }
    }

    private Vector2 SetSpawnPosition()
    {
        Vector2 spawnPos = _playerPosition.RuntimeValue;
        spawnPos += (Vector2)Random.insideUnitSphere.normalized * _spawnRadius.RuntimeValue;

        // Checks if Enemy is too close to Player 
        // and makes sure _spawnRadius is always greater than _offset to prevent Recursive from running endlessly 
        if ( _spawnRadius.RuntimeValue > _offset.RuntimeValue && Vector2.Distance(spawnPos, _playerPosition.RuntimeValue) < _offset.RuntimeValue && PreventSpawnOverlap(spawnPos)) spawnPos = SetSpawnPosition();

        return spawnPos;
    }

    private bool PreventSpawnOverlap(Vector3 spawnPos)
    {
        colliders = Physics2D.OverlapCircleAll(_playerPosition.RuntimeValue, _spawnRadius.RuntimeValue);

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
            {
                if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                {
                    return false;
                }
            }
        }

        return true;
    }

}
