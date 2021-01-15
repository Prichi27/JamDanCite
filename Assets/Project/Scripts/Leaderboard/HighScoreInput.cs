using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using System;

public class HighScoreInput : MonoBehaviour
{
    [SerializeField] private IntVariable _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private GameObject _inputGroup;
    [SerializeField] private GameObject _menuGroup;
    [SerializeField] private Button _submitButton;

    private void SetText()
    {
        _scoreText.text = _score.RuntimeValue.ToString();
    }

    private void OnEnable()
    {
        SetText();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void PostScore()
    {
        StartCoroutine("PostScoreCoroutine");
    }

    public IEnumerator PostScoreCoroutine()
    {
        _submitButton.interactable = false;

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

        _inputGroup.SetActive(false);
        _menuGroup.SetActive(true);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            _submitButton.interactable = true;
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        int nextScene = UnityEngine.Random.Range(2, 4);
        do
        {
            nextScene = UnityEngine.Random.Range(2, 4);

        } while (i == nextScene);
        SceneManager.LoadSceneAsync(nextScene);
    }

    public void Quit()
    {
        Application.Quit();
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
