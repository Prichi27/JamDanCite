using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private float _shakeTime;

    [SerializeField] private float _intensity = 5f;
    [SerializeField] private float _time = 5f;


    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        if (_camera)
        {
            _perlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        UnshakeCamera();
    }

    public void ShakeCamera()
    {
        _perlin.m_AmplitudeGain = _intensity;
        _shakeTime = _time;
    } 

    private void UnshakeCamera()
    {
        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;
            if (_shakeTime <= 0)
            {
                _perlin.m_AmplitudeGain = 0;
            }
        }
    }

}
