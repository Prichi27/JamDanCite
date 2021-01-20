using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private BoolVariable _isPlayerDead;
    [SerializeField] private BoolVariable _isGamePaused;

    private void Awake()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Space))) && !_isPlayerDead.RuntimeValue)
        {
            if(pausePanel.activeSelf )
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        _isGamePaused.RuntimeValue = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        _isGamePaused.RuntimeValue = false;
    }
}
