using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Gets reference to gameobject pool")]
    private GameObjectPool[] _enemyPools;

    [SerializeField]
    [Tooltip("The number of enemies in current wave")]
    private IntVariable _waveEnemy;

    [SerializeField]
    [Tooltip("The number of enemies in current wave")]
    private IntVariable _maxWave;

    [SerializeField]
    [Tooltip("Gets the current player location")]
    private Vector2Variable _spawnPosition;

    [SerializeField]
    [Tooltip("Radius within which player can be spawned")]
    private FloatVariable _spawnRadius;

    [SerializeField]
    [Tooltip("Radius within which player can be spawned")]
    private FloatVariable _colliderRadius;

    [SerializeField]
    [Tooltip("Radius from player where enemies cannot be spawned")]
    private FloatVariable _offset;

    [SerializeField]
    [Tooltip("Radius from player where enemies cannot be spawned")]
    private EnemyRuntimeSet _enemyRuntimeSet;

    [SerializeField]
    [Tooltip("Enemy Death Event")]
    private GameEvent _EnemyDeath;

    [SerializeField]
    [Tooltip("Wave number")]
    private IntVariable _waveNumber;

    [SerializeField]
    LayerMask _layerMask;

    //private int _waveNumber = 1;

    private int _currentIteration = 0;
    void Start()
    {
        SpawnEnemy();
    }

    /// <summary>
    /// Spawn enemies randomly on map
    /// </summary>
    public void SpawnEnemy()
    {
        _currentIteration = 0;
        for (int i = 0; i < _waveEnemy.RuntimeValue; i++)
        {
            _enemyPools[Random.Range(0, SpawnEnemyIndex())].GetPooledObject(SetSpawnPosition(), Quaternion.identity);
        }

        // Set enemy remaining
        _EnemyDeath.Raise();
    }

    public void NextWave()
    {
        if(_enemyRuntimeSet.Count() <= 0)
        {
            if(_waveEnemy.RuntimeValue < _maxWave.RuntimeValue) _waveEnemy.RuntimeValue += 5;
            _waveNumber.RuntimeValue++;
            SpawnEnemy();
        }
    }

    private int SpawnEnemyIndex()
    {
        return (int)Mathf.Ceil((float)_waveNumber.RuntimeValue / 2f) < _enemyPools.Length ? (int)Mathf.Ceil((float)_waveNumber.RuntimeValue / 2f) : _enemyPools.Length;
    }

    /// <summary>
    /// Calculates if gameobject can be spawned at current location
    /// </summary>
    /// <returns></returns>
    private Vector2 SetSpawnPosition()
    {
        Vector2 spawnPos = _spawnPosition.RuntimeValue;
        spawnPos += (Vector2)Random.insideUnitSphere.normalized * _spawnRadius.RuntimeValue;

        // Checks if Enemy is too close to Player 
        // and makes sure _spawnRadius is always greater than _offset to prevent Recursive from running endlessly 
        if (_colliderRadius.RuntimeValue > _offset.RuntimeValue && !PreventSpawnOverlap(spawnPos) && _currentIteration < 100)
        {
            spawnPos = SetSpawnPosition();
            _currentIteration++;
        } 

        return spawnPos;
    }

    private bool PreventSpawnOverlap(Vector3 spawnPos)
    {  
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, _colliderRadius.RuntimeValue, _layerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (Vector2.Distance(centerPoint, spawnPos) <= _offset.RuntimeValue) return false;

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
