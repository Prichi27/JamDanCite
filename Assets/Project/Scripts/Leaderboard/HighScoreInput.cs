using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class HighScoreInput : MonoBehaviour
{
    [SerializeField] private IntVariable _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TMP_InputField _username;

    private void SetText()
    {
        _scoreText.text = _score.RuntimeValue.ToString();
    }

    private void OnEnable()
    {
        SetText();
    }

    public void SetLeaderboard()
    {
        // Take user input and save to leaderboard
        string username = _username.text;

        if (username == "")
        {
            username = "player";
        }

        LeaderBoard.UpdateLeaderBoard(username, _score.RuntimeValue);
        for (int i = 0; i < LeaderBoard.MAX_RANK; i++)
        {
            var entry = LeaderBoard.FindAtIndex(i);

            Debug.Log("Name: " + entry.Name + ", Score: " + entry.Score);
        }
        //SceneManager.LoadScene("Main Menu");
    }
}
