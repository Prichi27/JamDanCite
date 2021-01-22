using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    private Image _image;

    [SerializeField] Sprite mute;
    [SerializeField] Sprite unMute;

    private void Awake()
    {
        _image = GetComponent<Image>();
        Debug.Log(_image);
    }

    public void MuteUnmuteSound()
    {
        AudioListener.pause = !AudioListener.pause;

        _image.sprite = AudioListener.pause ? unMute : mute;
    }
}
