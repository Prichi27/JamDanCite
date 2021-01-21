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
        if(_waveNumber.RuntimeValue % 7f == 0 && _enemyRuntimeSet.Count() == 1 )
        {
            int currentIndex = GetIndex();

            for(int i=0; i < currentIndex; i++)
            {
                if(_enemyStats[i].Speed < 1500) _enemyStats[i].Speed += 150;
            }
        }
    }

    private int GetIndex()
    {
        return (int)Mathf.Ceil((float)_waveNumber.RuntimeValue / 2f) < _enemyStats.Length ? (int)Mathf.Ceil((float)_waveNumber.RuntimeValue / 2f) : _enemyStats.Length;
    }

}
