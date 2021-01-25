using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _leaderBoardItem;
    [SerializeField] private GameObject _loading;
    [SerializeField] private Transform _leaderBoardParent;
    [SerializeField] private int _maxRank = 10;

    private UserList _userList;

    private void Awake()
    {
        StartCoroutine("GetLeaderboardCoroutine"); 
    }

    private void Start() 
    {        
        var entryGameobject = Instantiate(_leaderBoardItem);
        entryGameobject.transform.SetParent(_leaderBoardParent);

        LeaderBoardEntry entryScript = entryGameobject.GetComponent<LeaderBoardEntry>();
        entryScript.SetColours(221, 212, 118);
        entryScript.setHighcoreEntry("RANK", "NAME", "SCORE");
        _loading.SetActive(true);
    }

    public IEnumerator GetLeaderboardCoroutine()
    {

        UnityWebRequest www = new UnityWebRequest("https://jamdanssite-app.azurewebsites.net/api/scores/top/10", "GET");
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-ms-client-principal-id", "1234");
        www.SetRequestHeader("x-ms-client-principal-name", "bob");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                _userList = JsonUtility.FromJson<UserList>("{\"users\":" + www.downloadHandler.text + "}");
                //DisplayScore();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                throw;
            }
        }
    }

    private void DisplayScore()
    {
        for (int i = 0; i < _maxRank; i++)
        {
            var entryGameobject = Instantiate(_leaderBoardItem);
            entryGameobject.transform.SetParent(_leaderBoardParent);

            LeaderBoardEntry entryScript = entryGameobject.GetComponent<LeaderBoardEntry>();

            if (!entryScript) return;

            if (i < _userList.users.Length)
            {
                var entry = _userList.users[i];
                entryScript.setHighcoreEntry((i + 1).ToString(), entry.username, entry.value.ToString());
            }

            else
            {
                entryScript.setHighcoreEntry((i + 1).ToString(), "", "");
            }
        }
            
        _loading.SetActive(false);
    }

    [Serializable()]
    internal class User
    {
        public string _id;
        public int value;
        public string description;
        public string userId;
        public string username;
        public DateTime createdAt;
    }

    [Serializable()]
    internal class UserList
    {
        public User[] users;
    }
}
