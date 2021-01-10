using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Gets reference to gameobject pool")]
    private GameObjectPool[] _pickupPools;

    [SerializeField]
    [Tooltip("Gets the current player location")]
    private Vector2Variable _playerPosition;

    [SerializeField]
    [Tooltip("Radius within which player can be spawned")]
    private FloatVariable _spawnRadius;

    private GameObjectPool _lastPool;
    private GameObjectPool _currentPool;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPickup", 30.0f, 45.0f);
    }

    void SpawnPickup()
    {
        //_pickupPools[Random.Range(0, _pickupPools.Length)].GetPooledObject(SetSpawnPosition(), Quaternion.identity);
        while(_currentPool == _lastPool )
        {
            _currentPool = _pickupPools[Random.Range(0, _pickupPools.Length)];
        }
        _currentPool.GetPooledObject(SetSpawnPosition(), Quaternion.identity);
        _lastPool = _currentPool;
    }

    /// <summary>
    /// Calculates if gameobject can be spawned at current location
    /// </summary>
    /// <returns></returns>
    private Vector2 SetSpawnPosition()
    {
        Vector2 spawnPos = _playerPosition.RuntimeValue;
        spawnPos += (Vector2)Random.insideUnitSphere.normalized * _spawnRadius.RuntimeValue;

        return spawnPos;
    }
}
