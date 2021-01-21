using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    [SerializeField] private EnemyStats[] _enemyStats;
    [SerializeField] private IntVariable _waveNumber;
    [SerializeField] private EnemyRuntimeSet _enemyRuntimeSet;

    public void UpdateEnemyStats()
    {
        if(_waveNumber.RuntimeValue % 5f == 0 && _enemyRuntimeSet.Count() <= 0)
        {
            int currentIndex = GetIndex() - 1;

            for(int i=0; i < currentIndex; i++)
            {
                _enemyStats[i].Speed += 150;
                Debug.Log($"Speed increased for {_enemyStats[i].Name}");
                Debug.Log($"New speed: {_enemyStats[i].Speed}");
            }
        }
    }

    private int GetIndex()
    {
        return (int)Mathf.Ceil((float)_waveNumber.RuntimeValue / 2f) < _enemyStats.Length ? (int)Mathf.Ceil((float)_waveNumber.RuntimeValue / 2f) : _enemyStats.Length;
    }

}
