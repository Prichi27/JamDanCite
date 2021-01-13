using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardEntry : MonoBehaviour
{
    public TextMeshProUGUI rank;
    public TextMeshProUGUI username;
    public TextMeshProUGUI score;

    public void setHighcoreEntry(string rank, string username, string score)
    {
        this.rank.text = rank;
        this.username.text = username;
        this.score.text = score;
    }
}
