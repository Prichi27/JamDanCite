using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Awake() {
        _particle = GetComponent<ParticleSystem>();
    }
    private void OnEnable() {
        if(_particle) {
            _particle.Play();
        }
    }

    private void Update() {
        if(_particle && _particle.isStopped) {
            gameObject.SetActive(false);
        }
    }
}
