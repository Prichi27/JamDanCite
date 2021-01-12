using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class LeaderBoard
{
    public const int MAX_RANK = 10;
    private const string PLAYER_PREFS_BASE_KEY = "leaderboard";

    public class PlayerInfo
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public PlayerInfo(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }

    private static List<PlayerInfo> s_Scores;

    private static List<PlayerInfo> LeaderBoardEntries
    {
        get
        {
            if (s_Scores == null)
            {
                s_Scores = new List<PlayerInfo>();
                LoadLeaderBoard();
            }

            return s_Scores;
        }
    }

    private static void SortLeaderBoard()
    {
        s_Scores.Sort((a, b) => b.Score.CompareTo(a.Score));
    }

    private static void LoadLeaderBoard()
    {
        s_Scores.Clear();

        for (int i = 0; i < MAX_RANK; i++)
        {
            string infoName = PlayerPrefs.GetString(PLAYER_PREFS_BASE_KEY + "[" + i + "].name", "");
            int infoScore = PlayerPrefs.GetInt(PLAYER_PREFS_BASE_KEY + "[" + i + "].score", 0);
            PlayerInfo info = new PlayerInfo(infoName, infoScore);
            s_Scores.Add(info);
        }

        SortLeaderBoard();
    }

    public static void UpdateLeaderBoard(string name, int score)
    {
        LeaderBoardEntries.Add(new PlayerInfo(name, score));
        SortLeaderBoard();
        LeaderBoardEntries.RemoveAt(LeaderBoardEntries.Count - 1);

        for (int i = 0; i < MAX_RANK; i++)
        {
            PlayerInfo info = s_Scores[i];
            PlayerPrefs.SetString(PLAYER_PREFS_BASE_KEY + "[" + i + "].name", info.Name);
            PlayerPrefs.SetInt(PLAYER_PREFS_BASE_KEY + "[" + i + "].score", info.Score);
        }
    }

    public static PlayerInfo FindAtIndex(int index)
    {
        return LeaderBoardEntries[index];
    }

    public static List<PlayerInfo> GetLeaderBoard()
    {
        return LeaderBoardEntries;
    }

    public static void ClearLeaderBoard()
    {
        PlayerPrefs.DeleteAll();
        // Update LeaderBoardEntries
        GetLeaderBoard();
        LoadLeaderBoard();
    }
}
