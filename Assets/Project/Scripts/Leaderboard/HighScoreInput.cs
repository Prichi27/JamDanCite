using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using System;

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
        StartCoroutine("GetLeaderboardCoroutine");
    }

    public void PostScore()
    {
        StartCoroutine("PostScoreCoroutine");
    }

    public IEnumerator PostScoreCoroutine()
    {
        Score score = new Score();
        score.value = _score.RuntimeValue.ToString();
        score.description = _username.text;

        string json = JsonUtility.ToJson(score);

        UnityWebRequest www = new UnityWebRequest("https://jamdanssite-app.azurewebsites.net/api/scores", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("x-ms-client-principal-id", "1123");
        www.SetRequestHeader("x-ms-client-principal-name", _username.text);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
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

                UserList users = JsonUtility.FromJson<UserList>("{\"users\":" + www.downloadHandler.text + "}");
                for (int i = 0; i < users.users.Length; i++)
                {
                    Debug.Log($"Name: {users.users[i].username}, Score: {users.users[i].value}");
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                throw;
            }
        }
    }

    [Serializable()]
    internal class Score
    {
        public string value;
        public string description;
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
