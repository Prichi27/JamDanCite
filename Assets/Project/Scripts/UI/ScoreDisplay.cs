using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] IntVariable _score;
    [SerializeField] TextMeshProUGUI _scoreText;

    private void Awake()
    {
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        _scoreText.text = $"SCORE\n{_score.RuntimeValue}";
    }
}
