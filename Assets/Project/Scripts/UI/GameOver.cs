using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject _inputObject;
    public void SetActive()
    {
        Invoke("SetActiveInvoke", 0.5f);
    }

    public void SetActiveInvoke()
    {
        _inputObject.SetActive(true);
    }
}
