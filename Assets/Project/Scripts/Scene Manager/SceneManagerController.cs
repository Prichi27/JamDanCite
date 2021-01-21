using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void LoadScene(int i)
    {
        if(i == 0) Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }

    public void LoadRandomScene()
    {
        int nextScene = Random.Range(2, 4);
        LoadScene(nextScene);
    }
}
