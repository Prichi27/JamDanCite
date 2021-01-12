using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Test : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);

    [DllImport("__Internal")]
    public static extern void FetchData(string path, string objectName, string callback, string fallback);

    private void Start()
    {
        //GetJSON("example", gameObject.name, "OnRequestSucceess", "OnRequestFailure");
        FetchData("leaderboard", gameObject.name, "OnRequestSucceess", "OnRequestFailure");
    }

    private void OnRequestSucceess(string data)
    {
        Debug.Log(data);
        Debug.Log("test");
    }

    private void OnRequestFailure(string error)
    {
        Debug.LogError("NOT GOOD");
    }
}
