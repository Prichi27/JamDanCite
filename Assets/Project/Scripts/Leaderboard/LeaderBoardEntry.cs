using System;
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

    public void SetColours(byte r, byte g, byte b)
    {
        this.rank.color = new Color32(r, g, b, 255);
        this.username.color = new Color32(r, g, b, 255);
        this.score.color = new Color32(r, g, b, 255);
    }
}
