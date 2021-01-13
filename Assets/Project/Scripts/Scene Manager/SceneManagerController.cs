using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }

    public void LoadRandomScene()
    {
        int nextScene = Random.Range(2, 4);
        LoadScene(nextScene);
    }
}
