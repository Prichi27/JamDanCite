using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool[] pools;

    private void Awake()
    {
        foreach (var pool in pools)
        {
            pool.SpawnPool();
        }
    }
}
