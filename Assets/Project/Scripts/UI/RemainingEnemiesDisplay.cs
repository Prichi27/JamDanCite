using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingEnemiesDisplay : MonoBehaviour
{
    [SerializeField] EnemyRuntimeSet _enmies;
    [SerializeField] TextMeshProUGUI _text;

    private void Start()
    {
        Invoke("UpdateEnemyRemaining", 0.5f);
    }

    public void UpdateEnemyRemaining()
    {
        _text.text = $"ENEMIES REMAINING\n{_enmies.Count()}";
    }
}
