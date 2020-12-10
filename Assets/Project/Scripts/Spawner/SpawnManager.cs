using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject[] _spawnLocations;

    private GameObject _enemyParent;

    [SerializeField]
    private GameObject[] _enemies;

    [SerializeField]
    private IntVariable _waveEnemy;

    void Start()
    {
        _spawnLocations = GameObject.FindGameObjectsWithTag(Constants.SPAWNER);
        _enemyParent = GameObject.Find(Constants.ENEMIES_PARENT);

        for (int i = 0; i < _waveEnemy.RuntimeValue; i++)
        {
            Instantiate(_enemies[Random.Range(0, _enemies.Length)], _spawnLocations[Random.Range(0, _spawnLocations.Length)].transform.position, Quaternion.identity, _enemyParent.transform);
        }
    }

}
