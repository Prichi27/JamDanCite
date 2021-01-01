using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObjectPool pool;

    private void Awake()
    {
        pool.SpawnPool();
    }
}
