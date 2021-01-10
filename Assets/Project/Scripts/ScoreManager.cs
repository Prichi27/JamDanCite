using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable _score;

    public void AddScore(EnemyStats enemy)
    {
        _score.RuntimeValue += enemy.Score;
    }
}
