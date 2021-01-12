﻿using System.Collections;
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
    LayerMask _layerMask;

    void Start()
    {
        SpawnEnemy();
    }

    /// <summary>
    /// Spawn enemies randomly on map
    /// </summary>
    public void SpawnEnemy()
    {
        for (int i = 0; i < _waveEnemy.RuntimeValue; i++)
        {
            _enemyPools[Random.Range(0, _enemyPools.Length)].GetPooledObject(SetSpawnPosition(), Quaternion.identity);
        }
    }

    public void NextWave()
    {
        if(_enemyRuntimeSet.Count() <= 0)
        {
            _waveEnemy.RuntimeValue += 5;
            SpawnEnemy();
        }
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
        if (_colliderRadius.RuntimeValue > _offset.RuntimeValue && !PreventSpawnOverlap(spawnPos)) spawnPos = SetSpawnPosition();

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
